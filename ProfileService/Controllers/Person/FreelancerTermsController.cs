using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Person.Awards;
using ProfileService.Controllers.Common;
using ProfileService.Models.Person;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Person
{
    /// <summary>
    /// PersonAward controller
    /// </summary>
    [Route("api/person/freelance")]
    public class PersonFreelancerTermsController : BaseController
    {
        private readonly IPersonService _personService;

        /// <summary>    
        /// 
        /// </summary>
        /// <param name="personService"></param>
        public PersonFreelancerTermsController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// SEARCH persons
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<FreelanceTerms> Get(Guid personId)
        {
            return await _personService.GetFreelanceTermsAsync(personId);
        }

        /// <summary>
        /// CREATE a person terms
        /// </summary>
        /// <param name="terms"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<FreelanceTerms> Create(FreelanceTerms terms)
        {
            try
            {
                terms.Id = Guid.NewGuid();
                await _personService.AddFreelanceTermsAsync(terms);
                return terms;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a person terms
        /// </summary>
        /// <param name="terms"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<FreelanceTerms> Update([FromBody] FreelanceTerms terms)
        {
            try
            {
                await _personService.UpdateFreelanceTermsAsync(terms);
                return terms;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// DELETE a person terms
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid personId)
        {
            try
            {
                await _personService.DeleteFreelanceTermsAsync(personId);
                return Ok(new {personId});
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}