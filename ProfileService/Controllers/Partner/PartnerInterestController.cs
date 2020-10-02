using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Partner.Interest;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Partner
{
    /// <summary>
    /// PartnerInterest controller
    /// </summary>
    [Route("api/partner/interests")]
    public class PartnerInterestController : BaseController
    {
        private readonly IPartnerService _partnerService;

        public PartnerInterestController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        /// <summary>
        /// SEARCH partner interests
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetPartnerInterest>> Get(Guid partnerId)
        {
            return await _partnerService.GetInterestsAsync(partnerId);
        }

        /// <summary>
        /// CREATE a partner interest
        /// </summary>
        /// <param name="interest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Create([FromBody] NewPartnerInterest interest)
        {
            try
            {
                await _partnerService.AddInterestAsync(interest);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a partner interest
        /// </summary>
        /// <param name="interest"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] UpdatePartnerInterest interest)
        {
            try
            {
                await _partnerService.UpdateInterestAsync(interest);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE partner interest
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Delete(Guid id)
        {
            try
            {
                await _partnerService.DeleteInterestAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}