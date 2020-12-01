using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Person.Awards;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Person
{
    /// <summary>
    /// PersonAward controller
    /// </summary>
    [Route("api/person/awards")]
    public class PersonAwardController : BaseController
    {
        private readonly IPersonService _personService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personService"></param>
        public PersonAwardController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// SEARCH persons
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetPersonAward>> Get(Guid personId)
        {
            return await _personService.GetAwardsAsync(personId);
        }

        /// <summary>
        /// CREATE a person award
        /// </summary>
        /// <param name="award"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NewPersonAward> Create(NewPersonAward award)
        {
            try
            {
                await _personService.AddAwardAsync(award);
                return award;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a person award
        /// </summary>
        /// <param name="award"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdatePersonAward> Update([FromBody] UpdatePersonAward award)
        {
            try
            {
                await _personService.UpdateAwardAsync(award);
                return award;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE a person award
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _personService.DeleteAwardAsync(id);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}