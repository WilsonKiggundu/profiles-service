using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.FreelanceProject;
using ProfileService.Controllers.Common;
using ProfileService.Models;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers
{
    /// <summary>
    /// FreelanceProjectProject controller
    /// </summary>
    [Route("api/freelance/project/people")]
    public class FreelanceProjectPeopleController : BaseController
    {
        private readonly IFreelanceProjectService _service;
        private readonly ILogger<FreelanceProjectPeopleController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public FreelanceProjectPeopleController(IFreelanceProjectService service, ILogger<FreelanceProjectPeopleController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// SEARCH
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ICollection<FreelanceProjectHire>> Get(Guid projectId)
        {
            return await _service.GetHiresAsync(projectId);
        }

        /// <summary>
        /// CREATE
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<FreelanceProjectHire> Create(FreelanceProjectHire model)
        {
            try
            {
                model.Id = Guid.NewGuid();
                await _service.AddHireAsync(model);
                return model;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// UPDATE
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<FreelanceProjectHire> Update([FromBody] FreelanceProjectHire model)
        {
            try
            { 
                _logger.LogInformation(JsonConvert.SerializeObject(model));
                await _service.UpdateHireAsync(model);
                return model;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}