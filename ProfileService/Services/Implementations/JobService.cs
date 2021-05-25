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
        private readonly IPersonRepository _personRepository;
        private readonly IWebNotification _notification;
        private readonly IJobRepository _jobRepository;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<JobService> _logger;
        private readonly IWebHostEnvironment _environment;

        public JobService(IBusinessService businessService, IPersonService personService,
            IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<JobService> logger, IPersonRepository personRepository, IWebNotification notification, IWebHostEnvironment environment, IJobRepository jobRepository)
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
        }

        public async Task<ICollection<JobDto>> GetAsync(JobSearch search)
        {
            var baseUrl = _configuration.GetSection("JobsService:BaseUrl").Get<string>();

            if (search.JobId.HasValue)
            {
                var job = await _jobRepository.GetJobAsync(search.JobId.Value);
                search.Id = job.Reference;
            }
            
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

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);

            try
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                var jobs = new List<JobDto>();

                if (search?.Id != null)
                {
                    var job = JsonConvert.DeserializeObject<JobDto>(responseStream);

                    job.Profile = await _personService.GetByIdAsync(job.ProfileId);

                    if (string.IsNullOrEmpty(job.CompanyId))
                    {
                        var isGuid = Guid.TryParse(job.CompanyId, out var companyId);

                        if (isGuid)
                        {
                            job.Company = await _businessService.GetByIdAsync(companyId);
                        }
                        else
                        {
                            job.Company = new GetBusiness
                            {
                                Name = job.CompanyId
                            };
                        }
                    }

                    jobs.Add(job);
                }
                else
                {
                    var jobsJson = JsonConvert.DeserializeObject<List<JobDto>>(responseStream);

                    foreach (var job in jobsJson)
                    {
                        // _logger.LogInformation(JsonConvert.SerializeObject(job, Formatting.Indented));
                        //
                        // job.Profile = await _personService.GetByIdAsync(job.ProfileId);
                        if (string.IsNullOrEmpty(job.CompanyId)) continue;

                        var isGuid = Guid.TryParse(job.CompanyId, out var companyId);

                        if (isGuid)
                        {
                            job.Company = await _businessService.GetByIdAsync(companyId);
                        }
                        else
                        {
                            job.Company = new GetBusiness
                            {
                                Name = job.CompanyId
                            };
                        }
                    }

                    jobs.AddRange(jobsJson);
                }

                return jobs;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
                _logger.LogInformation("============= Reading response ===============");
                _logger.LogInformation(JsonConvert.SerializeObject(newJob, Formatting.Indented));
                
                // Save the job
                newJob.JobId = Guid.NewGuid();
                await _jobRepository.InsertAsync(new Job
                {
                    Id = newJob.JobId,
                    Title = job.Title,
                    ReplyEmail = job.ReplyEmail,
                    Reference = newJob.Id,
                    ProfileId = job.ProfileId
                });
                
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
            
            if (emails.Any())
            {
                emails.ForEach(async person =>
                {
                    var baseUrl = _configuration.GetSection("MyVillageBaseUrl").Get<string>();
                    var emailEndpoint = _configuration.GetSection("EmailEndpoint").Get<string>();
                    var emailTemplatePath = Path.Combine(_environment.ContentRootPath, "EmailTemplates/Jobs", "NewJobPosted.html");
                    
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

                    try
                    {
                        await client.PostAsync(emailEndpoint, content);
                    }
                    catch (Exception e)
                    {
                        _logger.LogCritical(e.Message);
                        Console.WriteLine(e);
                        throw;
                    }
                    
                });
                
            }
        }
    }
}