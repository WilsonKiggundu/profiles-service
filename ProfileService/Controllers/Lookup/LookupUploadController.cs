using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Lookup.Upload;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Lookup
{
    /// <summary>
    /// LookupUpload controller
    /// </summary>16c89a4e-03cc-4736-adc8-2ade45e15ac5
    [Route("api/lookup/uploads")]
    [AllowAnonymous]
    public class LookupUploadController : BaseController
    {
        private readonly ILookupUploadService _service;
        
        public LookupUploadController(ILookupUploadService service)
        {
            _service = service;
        }

        /// <summary>
        /// SEARCH lookup upload
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetLookupUpload>> Get(SearchLookupUpload request)
        {
            return await _service.SearchAsync(request);
        }

        /// <summary>
        /// CREATE a lookup upload
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Create([FromBody] NewLookupUpload upload)
        {
            try
            {
                await _service.InsertAsync(upload);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a lookup upload
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] UpdateLookupUpload upload)
        {
            try
            {
                await _service.UpdateAsync(upload);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE lookup upload
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