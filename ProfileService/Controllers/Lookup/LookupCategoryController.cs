using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Lookup.Category;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Lookup
{
    /// <summary>
    /// LookupCategory controller
    /// </summary>
    [Route("api/lookup/categories")]
    public class LookupCategoryController : BaseController
    {
        private readonly ILookupCategoryService _service;
        
        public LookupCategoryController(ILookupCategoryService service)
        {
            _service = service;
        }

        /// <summary>
        /// SEARCH lookup category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetLookupCategory>> Get(SearchLookupCategory request)
        {
            return await _service.SearchAsync(request);
        }

        /// <summary>
        /// CREATE a lookup category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Create([FromBody] NewLookupCategory category)
        {
            try
            {
                await _service.InsertAsync(category);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a lookup category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] UpdateLookupCategory category)
        {
            try
            {
                await _service.UpdateAsync(category);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE lookup category
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