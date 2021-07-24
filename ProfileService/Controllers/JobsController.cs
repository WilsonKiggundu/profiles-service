using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts;
using ProfileService.Controllers.Common;
using ProfileService.Models;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers
{
    [Route("/api/jobs")]
    public class JobsController : BaseController
    {
        private readonly IJobService _jobService;
        private readonly IJobApplicationService _jobApplicationService;
        private readonly ILogger<JobsController> _logger;
        private readonly IJobRepository _jobRepository;

        public JobsController(IJobService jobService, ILogger<JobsController> logger,
            IJobApplicationService jobApplicationService, IJobRepository jobRepository)
        {
            _jobService = jobService;
            _logger = logger;
            _jobApplicationService = jobApplicationService;
            _jobRepository = jobRepository;
        }

        [HttpGet]
        public async Task<ICollection<JobDto>> GetAsync([FromQuery] JobSearch search)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(search, Formatting.Indented));
            
            // if JobId is not null, get job from cache and pick reference
            if (search.JobId.HasValue)
            {
                var jobFromCache = await _jobRepository.GetJobAsync(search.JobId.Value);
                search.Id = jobFromCache?.Reference;
            }

            if (!search.Id.HasValue) return await _jobService.GetAsync(search);
            
            var jobs = new List<JobDto>();
            var job = await _jobService.GetByIdAsync(search.Id.Value);
            jobs.Add(job);
            return jobs;

        }

        [HttpPost]
        public async Task<JobDto> CreateAsync([FromBody] JobDto jobDto)
        {
            return await _jobService.CreateAsync(jobDto);
        }

        [HttpPut("application/update")]
        public async Task UpdateAsync([FromBody] JobApplication application)
        {
            await _jobApplicationService.UpdateAsync(application);
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyAsync([FromBody] JobApplication application)
        {
            await _jobApplicationService.InsertAsync(application);
            return Ok(application);
        }
    }
}