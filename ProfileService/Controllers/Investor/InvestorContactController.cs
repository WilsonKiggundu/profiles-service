using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Investor.Contact;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Investor
{
    /// <summary>
    /// InvestorContact controller
    /// </summary>
    [Route("api/investor/contacts")]
    public class InvestorContactController : BaseController
    {
        private readonly IInvestorService _investorService;

        public InvestorContactController(IInvestorService investorService)
        {
            _investorService = investorService;
        }

        /// <summary>
        /// SEARCH investor contacts
        /// </summary>
        /// <param name="investorId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetInvestorContact>> Get(Guid investorId)
        {
            return await _investorService.GetContactsAsync(investorId);
        }

        /// <summary>
        /// CREATE a investor contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Create([FromBody] NewInvestorContact contact)
        {
            try
            {
                await _investorService.AddContactAsync(contact);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a investor contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] UpdateInvestorContact contact)
        {
            try
            {
                await _investorService.UpdateContactAsync(contact);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE investor contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Delete(Guid id)
        {
            try
            {
                await _investorService.DeleteContactAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}