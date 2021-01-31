using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Person.Skills;
using ProfileService.Controllers.Common;
using ProfileService.Models.Person;
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
        private readonly ILogger<PersonSkillController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personService"></param>
        /// <param name="logger"></param>
        public PersonSkillController(IPersonService personService, ILogger<PersonSkillController> logger)
        {
            _personService = personService;
            _logger = logger;
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
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ICollection<PersonSkill>> Create(NewPersonSkill model)
        {
            try
            { 
                var response = new List<PersonSkill>();

                foreach (var skill in model.Skills)
                {    
                    var result = 
                        await _personService.AddSkillAsync(skill, model.PersonId);
                    response.Add(result);
                }
                
                return response;
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
        /// <param name="skillId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid skillId, Guid personId)
        {
            try
            {
                await _personService.DeleteSkillAsync(skillId, personId);
                return Ok(new {skillId, personId});
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}