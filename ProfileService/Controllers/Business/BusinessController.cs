using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Business;
using ProfileService.Controllers.Common;
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

        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        /// <summary>
        /// SEARCH businesses
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ICollection<GetBusiness>> Get(SearchBusiness request)
        {
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
        public async Task<NewBusiness> Create([FromBody] NewBusiness business)
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
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdateBusiness> Update([FromBody] UpdateBusiness business)
        {
            try
            {
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