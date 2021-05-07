using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Controllers.Common;
using ProfileService.Models.Person;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Person
{
    /// <summary>
    /// PersonStack controller
    /// </summary>
    [Route("api/person/stack")]
    public class PersonStackController : BaseController
    {
        private readonly IPersonService _personService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personService"></param>
        public PersonStackController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// SEARCH persons
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PersonStack>> Get(Guid personId)
        {
            return await _personService.GetStackAsync(personId);
        }

        /// <summary>
        /// CREATE a person stack
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PersonStack> Create(PersonStack stack)
        {
            try
            {
                stack.Id = Guid.NewGuid();
                await _personService.AddStackAsync(stack);
                return stack;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a person stack
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PersonStack> Update([FromBody] PersonStack stack)
        {
            try
            {
                await _personService.UpdateStackAsync(stack);
                return stack;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// DELETE a person stack
        /// </summary>
        /// <param name="stackId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid stackId, Guid personId)
        {
            try
            {
                await _personService.DeleteStackAsync(stackId, personId);
                return Ok(new {stackId, personId});
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}