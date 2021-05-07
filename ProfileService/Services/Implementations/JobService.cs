using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts;
using ProfileService.Contracts.Business;
using ProfileService.Services.Interfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ProfileService.Services.Implementations
{
    public class JobService : IJobService
    {
        private readonly IBusinessService _businessService;
        private readonly IPersonService _personService;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<JobService> _logger;

        public JobService(IBusinessService businessService, IPersonService personService, IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<JobService> logger)
        {
            _businessService = businessService;
            _personService = personService;
            _clientFactory = clientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ICollection<Job>> GetAsync(JobSearch search = null)
        {    
            var baseUrl = _configuration.GetSection("JobsService:BaseUrl").Get<string>();
            // var apiKey = _configuration.GetSection("JobsService:ApiKey").Get<string>();
            
            var url = $"{baseUrl}/api/jobs";

            if (search?.Id != null)
            {
                url = $"{url}/{search.Id}";
            }else if (search?.ProfileId != null)
            {
                url = $"{url}?profileId={search.ProfileId}";
            }else if (search?.CompanyId != null)
            {
                url = $"{url}?companyId={search.CompanyId}";
            }else if (search?.CompanyName != null)
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
    }
}