using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Business.Interest;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Business
{
    /// <summary>
    /// BusinessInterest controller
    /// </summary>
    [Route("api/business/interests")]
    public class BusinessInterestController : BaseController
    {
        private readonly IBusinessService _businessService;

        public BusinessInterestController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        /// <summary>
        /// SEARCH business interests
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetBusinessInterest>> Get(Guid businessId)
        {
            return await _businessService.GetInterestsAsync(businessId);
        }

        /// <summary>
        /// CREATE a business interest
        /// </summary>
        /// <param name="interest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Create([FromBody] NewBusinessInterest interest)
        {
            try
            {
                await _businessService.AddInterestAsync(interest);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a business interest
        /// </summary>
        /// <param name="interest"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] UpdateBusinessInterest interest)
        {
            try
            {
                await _businessService.UpdateInterestAsync(interest);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE business interest
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Delete(Guid id)
        {
            try
            {
                await _businessService.DeleteInterestAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}