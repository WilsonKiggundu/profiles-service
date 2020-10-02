using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Lookup.Need;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Lookup
{
    /// <summary>
    /// LookupNeed controller
    /// </summary>
    [Route("api/lookup/needs")]
    public class LookupNeedController : BaseController
    {
        private readonly ILookupNeedService _service;
        
        public LookupNeedController(ILookupNeedService service)
        {
            _service = service;
        }

        /// <summary>
        /// SEARCH lookup need
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetLookupNeed>> Get(SearchLookupNeed request)
        {
            return await _service.SearchAsync(request);
        }

        /// <summary>
        /// CREATE a lookup need
        /// </summary>
        /// <param name="need"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Create([FromBody] NewLookupNeed need)
        {
            try
            {
                await _service.InsertAsync(need);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a lookup need
        /// </summary>
        /// <param name="need"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] UpdateLookupNeed need)
        {
            try
            {
                await _service.UpdateAsync(need);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE lookup need
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