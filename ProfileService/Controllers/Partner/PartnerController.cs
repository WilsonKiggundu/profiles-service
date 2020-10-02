using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Partner;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Partner
{
    /// <summary>
    /// Partner controller
    /// </summary>
    [Route("api/partner")]
    public class PartnerController : BaseController
    {
        private readonly IPartnerService _partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        /// <summary>
        /// SEARCH partneres
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ICollection<GetPartner>> Get(SearchPartner request)
        {
            return await _partnerService.SearchAsync(request);
        }
        
        /// <summary>
        /// GET partner by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<GetPartner> GetOne(Guid id)
        {
            return await _partnerService.GetByIdAsync(id);
        }

        /// <summary>
        /// CREATE a partner
        /// </summary>
        /// <param name="partner"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NewPartner> Create([FromBody] NewPartner partner)
        {
            try
            {
                await _partnerService.InsertAsync(partner);
                return partner;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a partner
        /// </summary>
        /// <param name="partner"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdatePartner> Update([FromBody] UpdatePartner partner)
        {
            try
            {
                await _partnerService.UpdateAsync(partner);
                return partner;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE partner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _partnerService.DeleteAsync(id);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}