using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts;
using ProfileService.Controllers.Common;
using ProfileService.Models;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers
{
    [Route("/api/jobs")]
    public class JobsController : BaseController
    {
        private readonly IJobService _jobService;
        private readonly ILogger<JobsController> _logger;

        public JobsController(IJobService jobService, ILogger<JobsController> logger)
        {
            _jobService = jobService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ICollection<JobDto>> GetAsync([FromQuery]JobSearch search)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(search, Formatting.Indented));
            
            return await _jobService.GetAsync(search);
        }

        [HttpPost]
        public async Task<JobDto> CreateAsync([FromBody] JobDto jobDto)
        {
            // BackgroundJob.Enqueue(() => _jobService.CreateAsync(jobDto));
            return await _jobService.CreateAsync(jobDto);
        }

        [HttpPost("apply")]
        public async Task ApplyAsync([FromBody] JobApplication application)
        {
            // return await _jobService.CreateAsync(job);
        }
    }
}