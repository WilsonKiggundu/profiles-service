using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Partner.Contact;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Partner
{
    /// <summary>
    /// PartnerContact controller
    /// </summary>
    [Route("api/partner/contacts")]
    public class PartnerContactController : BaseController
    {
        private readonly IPartnerService _partnerService;

        public PartnerContactController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        /// <summary>
        /// SEARCH partner contacts
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetPartnerContact>> Get(Guid partnerId)
        {
            return await _partnerService.GetContactsAsync(partnerId);
        }

        /// <summary>
        /// CREATE a partner contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Create([FromBody] NewPartnerContact contact)
        {
            try
            {
                await _partnerService.AddContactAsync(contact);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a partner contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] UpdatePartnerContact contact)
        {
            try
            {
                await _partnerService.UpdateContactAsync(contact);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE partner contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Delete(Guid id)
        {
            try
            {
                await _partnerService.DeleteContactAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}