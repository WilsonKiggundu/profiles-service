using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        public IEnumerable<GetLookupCategory> Get()
        {
            return _service.SearchAsync();
        }

        /// <summary>
        /// CREATE a lookup category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NewLookupCategory> Create(NewLookupCategory category)
        {
            try
            {
                return await _service.InsertAsync(category);
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