using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Business;
using ProfileService.Contracts.Business.Role;
using ProfileService.Controllers.Common;
using ProfileService.Models.Business;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Business
{
    /// <summary>
    /// Business controller
    /// </summary>
    [Route("api/business")]
    public class BusinessController : BaseController
    {
        private readonly IBusinessService _businessService;
        private readonly ILogger<BusinessController> _logger;

        public BusinessController(IBusinessService businessService, ILogger<BusinessController> logger)
        {
            _businessService = businessService;
            _logger = logger;
        }

        /// <summary>
        /// SEARCH businesses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ICollection<GetBusiness>> Get()
        {
            var request = new SearchBusiness();
            return await _businessService.SearchAsync(request);
        }
        
        /// <summary>
        /// GET business by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<GetBusiness> GetOne(Guid id)
        {
            return await _businessService.GetByIdAsync(id);
        }

        /// <summary>
        /// CREATE a business
        /// </summary>
        /// <param name="business"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NewBusiness> Create(NewBusiness business)
        {
            try
            {
                await _businessService.InsertAsync(business);
                return business;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a business
        /// </summary>
        /// <param name="business"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdateBusiness> Update(UpdateBusiness business)
        {
            try
            {
                _logger.LogInformation(JsonConvert.SerializeObject(business));
                await _businessService.UpdateAsync(business);
                return business;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE business
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _businessService.DeleteAsync(id);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}