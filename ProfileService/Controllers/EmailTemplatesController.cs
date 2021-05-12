using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Controllers.Common;
using ProfileService.Models;
using ProfileService.Models.Common;
using ProfileService.Services.Implementations;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers
{
    [AllowAnonymous]
    [Route("api/email/template")]
    public class EmailTemplatesController : BaseController
    {
        private readonly IEmailTemplateService _service;

        public EmailTemplatesController(IEmailTemplateService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<EmailTemplate> GetById(Guid id)
        {
            try
            {
                return await _service.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        [HttpGet]
        public async Task<EmailTemplate> GetByType([FromQuery]EmailType type)
        {
            try
            {
                return await _service.GetByTypeAsync(type);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(EmailTemplate template)
        {
            try
            {
                await _service.InsertAsync(template);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        [HttpPut]    
        public async Task<IActionResult> Update(EmailTemplate template)
        {
            try
            {
                await _service.InsertAsync(template);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        [HttpDelete]    
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}