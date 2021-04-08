using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Controllers.Common;
using ProfileService.Models.Common;
using ProfileService.Services.Implementations;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers
{
    [AllowAnonymous]
    [Route("api/devices")]
    public class DevicesController : BaseController
    {
        private readonly IDeviceService _service;

        public DevicesController(IDeviceService service)
        {
            _service = service;
        }

        [HttpGet("key")]
        public async Task<string> GetKey()
        {
            var keys = await _service.GetVapidKeysAsync();
            return keys.PublicKey;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Register(Device device)
        {
            try
            {
                await _service.InsertAsync(device);
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