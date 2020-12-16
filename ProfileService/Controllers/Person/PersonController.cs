#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Person;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Person
{
    /// <summary>
    /// Person controller
    /// </summary>
    [Route("api/person")]
    public class PersonController : BaseController
    {
        private readonly IPersonService _personService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personService"></param>
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// SEARCH persons
        /// </summary>
        /// <param name="exclude"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ICollection<GetPerson>> Get(Guid? exclude = null)
        {
            return await _personService.SearchAsync(exclude);
        }
        
        /// <summary>
        /// GET person by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var profile = await _personService.GetByIdAsync(id);

            return Ok(profile);
        }

        /// <summary>
        /// CREATE a person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NewPerson> Create(NewPerson person)
        {
            try
            {
                await _personService.InsertAsync(person);
                if (person.Interests.Count > 0)
                {
                    
                }
                // await _personService.AddInterestAsync();
                return person;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdatePerson> Update([FromBody] UpdatePerson person)
        {
            try
            {
                await _personService.UpdateAsync(person);
                return person;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _personService.DeleteAsync(id);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}