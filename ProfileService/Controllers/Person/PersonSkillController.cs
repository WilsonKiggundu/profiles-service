using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Person.Skills;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Person
{
    /// <summary>
    /// PersonSkill controller
    /// </summary>
    [Route("api/person/skills")]
    public class PersonSkillController : BaseController
    {
        private readonly IPersonService _personService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personService"></param>
        public PersonSkillController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// SEARCH persons
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetPersonSkill>> Get(Guid personId)
        {
            return await _personService.GetSkillsAsync(personId);
        }

        /// <summary>
        /// CREATE a person skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NewPersonSkill> Create([FromBody] NewPersonSkill skill)
        {
            try
            {
                await _personService.AddSkillAsync(skill);
                return skill;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a person skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdatePersonSkill> Update([FromBody] UpdatePersonSkill skill)
        {
            try
            {
                await _personService.UpdateSkillAsync(skill);
                return skill;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE a person skill
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _personService.DeleteSkillAsync(id);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}