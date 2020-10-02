using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Investor.Portfolio;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Investor
{
    /// <summary>
    /// InvestorPortfolio controller
    /// </summary>
    [Route("api/investor/portfolios")]
    public class InvestorPortfolioController : BaseController
    {
        private readonly IInvestorService _investorService;

        public InvestorPortfolioController(IInvestorService investorService)
        {
            _investorService = investorService;
        }

        /// <summary>
        /// SEARCH investor portfolios
        /// </summary>
        /// <param name="investorId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetInvestorPortfolio>> Get(Guid investorId)
        {
            return await _investorService.GetPortfoliosAsync(investorId);
        }

        /// <summary>
        /// CREATE a investor address
        /// </summary>
        /// <param name="portfolio"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Create([FromBody] NewInvestorPortfolio portfolio)
        {
            try
            {
                await _investorService.AddPortfolioAsync(portfolio);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a investor address
        /// </summary>
        /// <param name="portfolio"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] UpdateInvestorPortfolio portfolio)
        {
            try
            {
                await _investorService.UpdatePortfolioAsync(portfolio);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE investor address
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Delete(Guid id)
        {
            try
            {
                await _investorService.DeletePortfolioAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}