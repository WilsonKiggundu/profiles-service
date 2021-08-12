using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Contracts.Business;
using ProfileService.Models;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ProfileService.Services.Implementations
{
    public class JobService : IJobService
    {
        private readonly IBusinessService _businessService;
        private readonly IPersonService _personService;
        private readonly IPostService _postService;
        private readonly IPersonRepository _personRepository;
        private readonly IWebNotification _notification;
        private readonly IJobRepository _jobRepository;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<JobService> _logger;
        private readonly IWebHostEnvironment _environment;

        public JobService(IBusinessService businessService, IPersonService personService,
            IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<JobService> logger, IPersonRepository personRepository, IWebNotification notification, IWebHostEnvironment environment, IJobRepository jobRepository, IPostService postService)
        {
            _businessService = businessService;
            _personService = personService;
            _clientFactory = clientFactory;
            _configuration = configuration;
            _logger = logger;
            _personRepository = personRepository;
            _notification = notification;
            _environment = environment;
            _jobRepository = jobRepository;
            _postService = postService;
        }
        
        public async Task<JobDto> GetByIdAsync(int id)
        {
            var baseUrl = _configuration.GetSection("JobsService:BaseUrl").Get<string>();
            var job = await _jobRepository.GetJobAsync(id);
            
            var search = new JobSearch { Id = id};
            var url = PrepareUrl(search, baseUrl);
            
            using var client = new HttpClient();

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
            
            var responseString = await response.Content.ReadAsStringAsync();
            
            var jobDetails = JsonConvert.DeserializeObject<JobDto>(responseString);

            try
            {
                jobDetails.Profile = job?.Profile;
                jobDetails.Company = job?.Company;
                jobDetails.JobId = job?.Id;
                jobDetails.Applications = job?.Applications.Select(application => new JobApplicantProfile
                {
                    Id = application.ApplicantId,
                    Name = $"{application.Applicant?.Firstname} {application.Applicant?.Lastname}",
                    ApplicationId = application.Id,
                    JobId = application.JobId,
                    Avatar = application.Applicant?.Avatar,
                    DateTime = application.DateCreated,
                    Status = application.Status
                    
                }).ToList();
                
                return jobDetails;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<ICollection<JobDto>> GetAsync(JobSearch search)
        {
            var baseUrl = _configuration.GetSection("JobsService:BaseUrl").Get<string>();
            
            var url = PrepareUrl(search, baseUrl);
            
            using var client = new HttpClient();

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
            
            var responseString = await response.Content.ReadAsStringAsync();
            var jobs = JsonConvert.DeserializeObject<ICollection<JobDto>>(responseString);
            
            var jobIds = jobs.Select(s => s.Id).ToList();

            var jobReferences = await _jobRepository.GetManyAsync(jobIds);
            
            jobs.ToList().ForEach(job =>
            {
                var reference = jobReferences
                    .FirstOrDefault(q => q.Reference == job.Id);
                
                job.JobId = reference?.Id;
                job.Profile = reference?.Profile;
                job.Company = reference?.Company;
            });

            return jobs;
        }

        private static string PrepareUrl(JobSearch search, string baseUrl)
        {
            var url = $"{baseUrl}/api/jobs";

            if (search.Id != null)
            {
                url = $"{url}/{search.Id}";
            }
            else if (search.ProfileId != null)
            {
                url = $"{url}?profileId={search.ProfileId}";
            }
            else if (search.CompanyId != null)
            {
                url = $"{url}?companyId={search.CompanyId}";
            }
            else if (search.CompanyName != null)
            {
                url = $"{url}?companyId={search.CompanyName}";
            }

            return url;
        }

        public async Task<JobDto> CreateAsync(JobDto job)
        {
            var jobsUrl = _configuration.GetSection("JobsService:BaseUrl").Get<string>();
            jobsUrl = $"{jobsUrl}/api/jobs";
            
            try
            {
                using var client = new HttpClient();

                var json = JsonConvert.SerializeObject(job);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(jobsUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();

                var newJob = JsonConvert.DeserializeObject<JobDto>(responseString);
                
                // Save the job
                newJob.JobId = Guid.NewGuid();
                await _jobRepository.InsertAsync(new Job
                {
                    Id = newJob.JobId.Value,
                    Title = job.Title,
                    ReplyEmail = job.ReplyEmail,
                    Reference = newJob.Id,
                    ProfileId = job.ProfileId
                });
                
                var newPost = new NewPost
                {
                    Type = PostType.Job,
                    AuthorId = newJob.ProfileId,
                    Title = newJob.Title,
                    Details = newJob.Details,
                    ReferenceId = newJob.JobId
                };
            
                // add post
                BackgroundJob.Enqueue(() => _postService.InsertAsync(newPost));
                
                // send email notification
                BackgroundJob.Enqueue(() => ProcessEmail(newJob));
                
                // send app notification
                
                return job;

            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                Console.WriteLine(e);
                throw;
            }
        }

        public void ProcessEmail(JobDto job)
        {
            var emails = _personRepository
                .GetAll()
                .Where(q => !string.IsNullOrEmpty(q.Email))
                .Select(s => new
                {
                    s.Firstname,
                    s.Lastname,
                    s.Email
                }).ToList();
            
            _logger.LogInformation($"{emails.Count} email addresses found");
            
            if (emails.Any())
            {
                emails.ForEach(async person =>
                {
                    _logger.LogInformation($"Sending email to {person.Email}");
                    var baseUrl = _configuration.GetSection("MyVillageBaseUrl").Get<string>();
                    var emailEndpoint = _configuration.GetSection("EmailEndpoint").Get<string>();
                    var emailTemplatePath = Path.Combine(_environment.WebRootPath, "EmailTemplates", "Jobs", "NewJobPosted.html");

                    try
                    {
                        var body = await File.ReadAllTextAsync(emailTemplatePath);    

                        body = body
                            .Replace("[PERSON_NAME]", $"{person.Firstname}")
                            .Replace("[JOB_TITLE]", $"{job.Title}")
                            .Replace("[JOB_URL]", $"{baseUrl}/jobs/{job.JobId}/details");

                        var emailDetails = new EmailDetailsDto
                        {
                            Body = body,
                            Subject = "New job posted",
                            Recipient = person.Email
                        };
                    
                        using var client = new HttpClient();

                        var json = JsonConvert.SerializeObject(emailDetails, Formatting.Indented);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        await client.PostAsync(emailEndpoint, content);
                        _logger.LogInformation("Email sent");
                    }
                    catch (Exception e)
                    {
                        _logger.LogCritical($"Error while sending email {e.Message}");
                        throw new Exception(e.Message, e);
                    }
                    
                });
                
            }
        }
    }
}