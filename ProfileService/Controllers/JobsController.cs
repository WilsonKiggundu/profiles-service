using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers
{
    [Route("/api/jobs")]
    public class JobsController : BaseController
    {
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public async Task<ICollection<Job>> GetAsync([FromQuery]JobSearch search)
        {
            return await _jobService.GetAsync(search);
        }
    }
}