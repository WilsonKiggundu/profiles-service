using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Investor.Address;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Investor
{
    /// <summary>
    /// InvestorAddress controller
    /// </summary>
    [Route("api/investor/addresses")]
    public class InvestorAddressController : BaseController
    {
        private readonly IInvestorService _investorService;

        public InvestorAddressController(IInvestorService investorService)
        {
            _investorService = investorService;
        }

        /// <summary>
        /// SEARCH investor addresses
        /// </summary>
        /// <param name="investorId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetInvestorAddress>> Get(Guid investorId)
        {
            return await _investorService.GetAddressesAsync(investorId);
        }

        /// <summary>
        /// CREATE a investor address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Create([FromBody] NewInvestorAddress address)
        {
            try
            {
                await _investorService.AddAddressAsync(address);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a investor address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] UpdateInvestorAddress address)
        {
            try
            {
                await _investorService.UpdateAddressAsync(address);
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
                await _investorService.DeleteAddressAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}