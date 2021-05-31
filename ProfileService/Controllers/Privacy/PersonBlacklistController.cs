#nullable enable
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Person;
using ProfileService.Controllers.Common;
using ProfileService.Models.Preferences;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Privacy
{
    /// <summary>
    /// Person Blacklist controller
    /// </summary>
    [Route("api/person/blacklist")]
    public class PersonBlacklistController : BaseController
    {
        private readonly IPersonBlacklistService _service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public PersonBlacklistController(IPersonBlacklistService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get person blacklists
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PersonBlacklist>> Get([FromQuery] Guid personId)
        {
            return await _service.GetByIdAsync(personId);
        }

        /// <summary>
        /// Add to blacklist
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>    
        [HttpPost]
        public async Task<IActionResult> Create(PersonBlacklist model)
        {
            try
            {
                await _service.InsertAsync(model);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// Remove from blacklist
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="blacklistId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid personId, Guid blacklistId)
        {
            try
            {
                await _service.DeleteAsync(personId, blacklistId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}