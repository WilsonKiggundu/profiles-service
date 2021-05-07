using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.FreelanceProject;
using ProfileService.Controllers.Common;
using ProfileService.Models;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers
{
    /// <summary>
    /// FreelanceProjectProject controller
    /// </summary>
    [Route("api/freelance/project")]
    public class FreelanceProjectController : BaseController
    {
        private readonly IFreelanceProjectService _service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public FreelanceProjectController(IFreelanceProjectService service)
        {
            _service = service;
        }

        /// <summary>
        /// SEARCH freelance projects
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<FreelanceProject> Get(Guid id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpGet]
        public async Task<SearchFreelanceProjectResponse> SearchAsync([FromQuery] SearchFreelanceProjectRequest request)
        {
            return await _service.SearchAsync(request);
        }

        /// <summary>
        /// CREATE a freelance project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<FreelanceProject> Create(FreelanceProject project)
        {
            try
            {
                project.Id = Guid.NewGuid();
                return await _service.InsertAsync(project);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a freelance project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<FreelanceProject> Update([FromBody] FreelanceProject project)
        {
            try
            {
                return await _service.UpdateAsync(project);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// DELETE a freelance project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok(new {id});
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}