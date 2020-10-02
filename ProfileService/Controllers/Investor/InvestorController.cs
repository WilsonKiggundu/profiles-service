using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Investor;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Investor
{
    /// <summary>
    /// Investor controller
    /// </summary>
    [Route("api/investor")]
    public class InvestorController : BaseController
    {
        private readonly IInvestorService _investorService;

        public InvestorController(IInvestorService investorService)
        {
            _investorService = investorService;
        }

        /// <summary>
        /// SEARCH investors
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ICollection<GetInvestor>> Get(SearchInvestor request)
        {
            return await _investorService.SearchAsync(request);
        }
        
        /// <summary>
        /// GET an investor by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<GetInvestor> GetOne(Guid id)
        {
            return await _investorService.GetByIdAsync(id);
        }

        /// <summary>
        /// CREATE an investor
        /// </summary>
        /// <param name="investor"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NewInvestor> Create([FromBody] NewInvestor investor)
        {
            try
            {
                await _investorService.InsertAsync(investor);
                return investor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE an investor
        /// </summary>
        /// <param name="investor"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdateInvestor> Update([FromBody] UpdateInvestor investor)
        {
            try
            {
                await _investorService.UpdateAsync(investor);
                return investor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE an investor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _investorService.DeleteAsync(id);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}