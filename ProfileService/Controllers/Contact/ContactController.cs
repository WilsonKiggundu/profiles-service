using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Business;
using ProfileService.Contracts.Contact;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Contact
{
    /// <summary>
    /// Contact controller
    /// </summary>
    [Route("api/contact")]
    public class ContactController : BaseController
    {
        private readonly IContactService _contactService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactService"></param>
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        /// <summary>
        /// SEARCH contacts
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ICollection<GetContact>> Get(SearchContact request)
        {
            return await _contactService.SearchAsync(request);
        }
        
        /// <summary>
        /// GET contact by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<GetContact> GetOne(Guid id)
        {
            return await _contactService.GetByIdAsync(id);
        }

        /// <summary>
        /// CREATE a contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NewContact> Create([FromBody] NewContact contact)
        {
            try
            {
                await _contactService.InsertAsync(contact);
                return contact;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdateContact> Update([FromBody] UpdateContact contact)
        {
            try
            {
                await _contactService.UpdateAsync(contact);
                return contact;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _contactService.DeleteAsync(id);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}