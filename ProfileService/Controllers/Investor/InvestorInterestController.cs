using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Investor.Interest;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Investor
{
    /// <summary>
    /// InvestorInterest controller
    /// </summary>
    [Route("api/investor/interests")]
    public class InvestorInterestController : BaseController
    {
        private readonly IInvestorService _investorService;

        public InvestorInterestController(IInvestorService investorService)
        {
            _investorService = investorService;
        }

        /// <summary>
        /// SEARCH investor interests
        /// </summary>
        /// <param name="investorId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetInvestorInterest>> Get(Guid investorId)
        {
            return await _investorService.GetInterestsAsync(investorId);
        }

        /// <summary>
        /// CREATE an investor interest
        /// </summary>
        /// <param name="interest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Create([FromBody] NewInvestorInterest interest)
        {
            try
            {
                await _investorService.AddInterestAsync(interest);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE an investor interest
        /// </summary>
        /// <param name="interest"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] UpdateInvestorInterest interest)
        {
            try
            {
                await _investorService.UpdateInterestAsync(interest);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE investor interest
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Delete(Guid id)
        {
            try
            {
                await _investorService.DeleteInterestAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}