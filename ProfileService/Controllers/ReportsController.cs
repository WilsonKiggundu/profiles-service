using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers
{
    [Route("api/reports")]
    public class ReportsController : BaseController
    {
        private readonly IPersonService _personService;

        public ReportsController(IPersonService personService)
        {
            _personService = personService;
        }
        
        // /api/reports/persons/count

        [HttpGet("persons/count")]
        public async Task<IActionResult> Index()
        {
            var count = await _personService.CountAllAsync();
            return Ok(new {Count = count});
        }
    }
}