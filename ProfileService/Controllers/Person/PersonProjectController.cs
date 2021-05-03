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
    /// PersonProject controller
    /// </summary>
    [Route("api/person/project")]
    public class PersonProjectController : BaseController
    {
        private readonly IPersonService _personService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personService"></param>
        public PersonProjectController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// SEARCH persons
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PersonProject>> Get(Guid personId)
        {
            return await _personService.GetProjectsAsync(personId);
        }

        /// <summary>
        /// CREATE a person project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PersonProject> Create(PersonProject project)
        {
            try
            {
                project.Id = Guid.NewGuid();
                await _personService.AddProjectAsync(project);
                return project;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a person project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PersonProject> Update([FromBody] PersonProject project)
        {
            try
            {
                await _personService.UpdateProjectAsync(project);
                return project;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// DELETE a person project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid projectId, Guid personId)
        {
            try
            {
                await _personService.DeleteProjectAsync(projectId, personId);
                return Ok(new {projectId, personId});
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}