using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Common;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;
using SendGrid;

namespace ProfileService.Controllers
{
    [Route("api/messaging")]
    public class MessagingController : BaseController
    {
        private readonly IEmailService _emailService;

        public MessagingController(ISendGridClient client, IEmailService emailService)
        {
            _emailService = emailService;
        }

        // GET    
        [AllowAnonymous]
        [HttpPost("send/email")]
        public async Task<IActionResult> SendEmail(EmailDetails emailDetails)
        {
            await _emailService.SendEmailAsync(emailDetails);
            return Ok();
        }
    }
}