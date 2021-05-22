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
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<JobService> _logger;
        private readonly IWebHostEnvironment _environment;

        public JobService(IBusinessService businessService, IPersonService personService,
            IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<JobService> logger, IPersonRepository personRepository, IWebNotification notification, IWebHostEnvironment environment)
        {
            _businessService = businessService;
            _personService = personService;
            _clientFactory = clientFactory;
            _configuration = configuration;
            _logger = logger;
            _personRepository = personRepository;
            _notification = notification;
            _environment = environment;
        }

        public async Task<ICollection<Job>> GetAsync(JobSearch search = null)
        {
            var baseUrl = _configuration.GetSection("JobsService:BaseUrl").Get<string>();
            // var apiKey = _configuration.GetSection("JobsService:ApiKey").Get<string>();

            var url = $"{baseUrl}/api/jobs";

            if (search?.Id != null)
            {
                url = $"{url}/{search.Id}";
            }
            else if (search?.ProfileId != null)
            {
                url = $"{url}?profileId={search.ProfileId}";
            }
            else if (search?.CompanyId != null)
            {
                url = $"{url}?companyId={search.CompanyId}";
            }
            else if (search?.CompanyName != null)
            {
                url = $"{url}?companyId={search.CompanyName}";
            }

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");
            // request.Headers.Add("APIKEY", apiKey);

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);

            try
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                var jobs = new List<Job>();

                if (search?.Id != null)
                {
                    var job = JsonConvert.DeserializeObject<Job>(responseStream);

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
                    var jobsJson = JsonConvert.DeserializeObject<List<Job>>(responseStream);

                    foreach (var job in jobsJson)
                    {
                        job.Profile = await _personService.GetByIdAsync(job.ProfileId);
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

        public async Task<Job> CreateAsync(Job job)
        {
            var jobsUrl = _configuration.GetSection("JobsService:BaseUrl").Get<string>();
            jobsUrl = $"{jobsUrl}/api/jobs";

            var json = JsonConvert.SerializeObject(job, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            try
            {
                using var client = new HttpClient();
                await client.PostAsync(jobsUrl, content);

                BackgroundJob.Enqueue(() => ProcessEmail(job));

                return job;

            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                Console.WriteLine(e);
                throw;
            }
        }

        private void ProcessEmail(Job job)
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
                        .Replace("[JOB_URL]", $"{baseUrl}/jobs/{job.Id}/details");

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