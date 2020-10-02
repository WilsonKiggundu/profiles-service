using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Partner.Contribution;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Partner
{
    /// <summary>
    /// PartnerContribution controller
    /// </summary>
    [Route("api/business/contributions")]
    public class PartnerContributionController : BaseController
    {
        private readonly IPartnerService _businessService;

        public PartnerContributionController(IPartnerService businessService)
        {
            _businessService = businessService;
        }

        /// <summary>
        /// SEARCH business contributions
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetPartnerContribution>> Get(Guid businessId)
        {
            return await _businessService.GetContributionsAsync(businessId);
        }

        /// <summary>
        /// CREATE a business contribution
        /// </summary>
        /// <param name="contribution"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Create([FromBody] NewPartnerContribution contribution)
        {
            try
            {
                await _businessService.AddContributionAsync(contribution);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a business contribution
        /// </summary>
        /// <param name="contribution"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] UpdatePartnerContribution contribution)
        {
            try
            {
                await _businessService.UpdateContributionAsync(contribution);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE business contribution
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Delete(Guid id)
        {
            try
            {
                await _businessService.DeleteContributionAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}