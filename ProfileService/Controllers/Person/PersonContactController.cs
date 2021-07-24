using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Person.Contact;
using ProfileService.Controllers.Common;
using ProfileService.Models.Person;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Person
{
    /// <summary>
    /// PersonContact controller
    /// </summary>
    [Route("api/person/contacts")]
    public class PersonContactController : BaseController
    {
        private readonly IPersonService _personService;

        public PersonContactController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// SEARCH person contacts
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetPersonContact>> Get(Guid personId)
        {
            return await _personService.GetContactsAsync(personId);
        }

        /// <summary>
        /// CREATE a person contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PersonContact> Create([FromBody] NewPersonContact contact)
        {
            try
            {
                return await _personService.AddContactAsync(contact);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a person contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PersonContact> Update([FromBody] UpdatePersonContact contact)
        {
            try
            {
                return await _personService.UpdateContactAsync(contact);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// DELETE person contact
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
                await _personService.DeleteContactAsync(contactId, belongsTo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}