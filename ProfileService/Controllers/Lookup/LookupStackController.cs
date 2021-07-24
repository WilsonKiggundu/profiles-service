using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Controllers.Common;
using ProfileService.Models.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Lookup
{
    /// <summary>
    /// LookupTechStack controller
    /// </summary>
    [Route("api/lookup/stack")]
    public class LookupTechStackController : BaseController
    {
        private readonly ILookupStackService _service;
        
        public LookupTechStackController(ILookupStackService service)
        {
            _service = service;
        }

        /// <summary>
        /// SEARCH lookup stack
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<TechStack>> Get(string name)
        {
            return await _service.SearchAsync(name);
        }

        /// <summary>
        /// CREATE a lookup stack
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Create(TechStack stack)
        {
            try
            {
                await _service.InsertAsync(stack);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a lookup stack
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] TechStack stack)
        {
            try
            {
                await _service.UpdateAsync(stack);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE lookup stack
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