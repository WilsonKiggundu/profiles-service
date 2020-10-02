using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Partner.Portfolio;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Partner
{
    /// <summary>
    /// PartnerPortfolio controller
    /// </summary>
    [Route("api/partner/portfolios")]
    public class PartnerPortfolioController : BaseController
    {
        private readonly IPartnerService _partnerService;

        public PartnerPortfolioController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        /// <summary>
        /// SEARCH partner portfolios
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetPartnerPortfolio>> Get(Guid partnerId)
        {
            return await _partnerService.GetPortfoliosAsync(partnerId);
        }

        /// <summary>
        /// CREATE a partner portfolio
        /// </summary>
        /// <param name="portfolio"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Create([FromBody] NewPartnerPortfolio portfolio)
        {
            try
            {
                await _partnerService.AddPortfolioAsync(portfolio);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a partner portfolio
        /// </summary>
        /// <param name="portfolio"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] UpdatePartnerPortfolio portfolio)
        {
            try
            {
                await _partnerService.UpdatePortfolioAsync(portfolio);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE partner portfolio
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Delete(Guid id)
        {
            try
            {
                await _partnerService.DeletePortfolioAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}