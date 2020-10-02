using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Lookup.Interest;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Lookup
{
    /// <summary>
    /// LookupInterest controller
    /// </summary>
    [Route("api/lookup/interests")]
    public class LookupInterestController : BaseController
    {
        private readonly ILookupInterestService _service;
        
        public LookupInterestController(ILookupInterestService service)
        {
            _service = service;
        }

        /// <summary>
        /// SEARCH lookup interest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetLookupInterest>> Get(SearchLookupInterest request)
        {
            return await _service.SearchAsync(request);
        }

        /// <summary>
        /// CREATE a lookup interest
        /// </summary>
        /// <param name="interest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Create([FromBody] NewLookupInterest interest)
        {
            try
            {
                await _service.InsertAsync(interest);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a lookup interest
        /// </summary>
        /// <param name="interest"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] UpdateLookupInterest interest)
        {
            try
            {
                await _service.UpdateAsync(interest);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE lookup interest
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