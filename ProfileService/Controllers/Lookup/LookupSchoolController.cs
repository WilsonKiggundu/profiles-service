using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Controllers.Common;
using ProfileService.Models.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Lookup
{
    /// <summary>
    /// LookupSchool controller
    /// </summary>
    [Route("api/lookup/schools")]
    public class LookupSchoolController : BaseController
    {
        private readonly ILookupSchoolService _service;
        
        public LookupSchoolController(ILookupSchoolService service)
        {
            _service = service;
        }

        /// <summary>
        /// SEARCH lookup school
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<School>> Get(string name)
        {
            return await _service.SearchAsync(name);
        }

        /// <summary>
        /// CREATE a lookup school
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Create(School school)
        {
            try
            {
                await _service.InsertAsync(school);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a lookup school
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] School school)
        {
            try
            {
                await _service.UpdateAsync(school);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE lookup school
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}