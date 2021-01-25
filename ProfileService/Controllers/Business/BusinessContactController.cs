using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Business.Contact;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Business
{
    /// <summary>
    /// BusinessContact controller
    /// </summary>
    [Route("api/business/contacts")]
    public class BusinessContactController : BaseController
    {
        private readonly IBusinessService _businessService;

        public BusinessContactController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        /// <summary>
        /// SEARCH business contacts
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetBusinessContact>> Get(Guid businessId)
        {
            return await _businessService.GetContactsAsync(businessId);
        }

        /// <summary>
        /// CREATE a business contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NewBusinessContact> Create([FromBody] NewBusinessContact contact)
        {
            try
            {
                return await _businessService.AddContactAsync(contact);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a business contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] UpdateBusinessContact contact)
        {
            try
            {
                await _businessService.UpdateContactAsync(contact);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// DELETE business contact
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="belongsTo"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Delete(Guid contactId, Guid belongsTo)
        {
            try
            {
                await _businessService.DeleteContactAsync(contactId, belongsTo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}