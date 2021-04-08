using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Business.Role;
using ProfileService.Controllers.Common;
using ProfileService.Models.Business;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Business
{
    /// <summary>
    /// BusinessRole controller
    /// </summary>
    [Route("api/business/roles")]
    [AllowAnonymous]
    public class BusinessRoleController : BaseController
    {
        private readonly IBusinessService _businessService;
        private readonly ILogger<BusinessRoleController> _logger;

        public BusinessRoleController(IBusinessService businessService, ILogger<BusinessRoleController> logger)
        {
            _businessService = businessService;
            _logger = logger;
        }

        /// <summary>
        /// SEARCH business roles
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetBusinessRole>> Get(Guid businessId)
        {
            return await _businessService.GetRolesAsync(businessId);
        }

        /// <summary>
        /// CREATE a business role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BusinessRole> Create(NewBusinessRole role)
        {
            try
            {
                return await _businessService.AddRoleAsync(role);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a business role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] UpdateBusinessRole role)
        {
            try
            {
                await _businessService.UpdateRoleAsync(role);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// DELETE business role
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Delete(Guid businessId, Guid personId)
        {
            try
            {
                await _businessService.DeleteRoleAsync(businessId, personId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}