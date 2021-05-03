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
    /// PersonEmployment controller
    /// </summary>
    [Route("api/person/employment")]
    public class PersonEmploymentController : BaseController
    {
        private readonly IPersonService _personService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personService"></param>
        public PersonEmploymentController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// SEARCH persons
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PersonEmployment>> Get(Guid personId)
        {
            return await _personService.GetEmploymentAsync(personId);
        }

        /// <summary>
        /// CREATE a person employment
        /// </summary>
        /// <param name="employment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PersonEmployment> Create(PersonEmployment employment)
        {
            try
            {
                employment.Id = Guid.NewGuid();
                await _personService.AddEmploymentAsync(employment);
                return employment;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a person employment
        /// </summary>
        /// <param name="employment"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PersonEmployment> Update([FromBody] PersonEmployment employment)
        {
            try
            {
                await _personService.UpdateEmploymentAsync(employment);
                return employment;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// DELETE a person employment
        /// </summary>
        /// <param name="employmentId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid employmentId, Guid personId)
        {
            try
            {
                await _personService.DeleteEmploymentAsync(employmentId, personId);
                return Ok(new {employmentId, personId});
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}