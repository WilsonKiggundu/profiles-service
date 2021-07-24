using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers
{
    [Route("api/reports")]
    public class ReportsController : BaseController
    {
        private readonly IPersonService _personService;
        private readonly ILogsService _logsService;

        public ReportsController(IPersonService personService, ILogsService logsService)
        {
            _personService = personService;
            _logsService = logsService;
        }
        
        // /api/reports/persons/count

        /// <summary>
        /// GET Count all persons
        /// </summary>
        /// <returns></returns>
        [HttpGet("persons/count")]
        public async Task<IActionResult> Index()
        {
            var count = await _personService.CountAllAsync();
            return Ok(new {Count = count});
        }

        /// <summary>
        /// Audit Logs
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("logs")]
        public async Task<LogsDto> GetLogs(int page = 1, int pageSize = 100)
        {
            var count = await _logsService.CountAsync(page, pageSize);
            var logs = await _logsService.SearchAsync(page, pageSize);

            return new LogsDto
            {
                Total = count, 
                Logs = logs
            };
        }
    }
}