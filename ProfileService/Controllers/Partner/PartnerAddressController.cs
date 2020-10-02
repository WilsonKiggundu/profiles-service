using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Partner.Address;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Partner
{
    /// <summary>
    /// PartnerAddress controller
    /// </summary>
    [Route("api/partner/addresses")]
    public class PartnerAddressController : BaseController
    {
        private readonly IPartnerService _partnerService;

        public PartnerAddressController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        /// <summary>
        /// SEARCH partner addresses
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetPartnerAddress>> Get(Guid partnerId)
        {
            return await _partnerService.GetAddressesAsync(partnerId);
        }

        /// <summary>
        /// CREATE a partner address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Create([FromBody] NewPartnerAddress address)
        {
            try
            {
                await _partnerService.AddAddressAsync(address);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a partner address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] UpdatePartnerAddress address)
        {
            try
            {
                await _partnerService.UpdateAddressAsync(address);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE partner address
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Delete(Guid id)
        {
            try
            {
                await _partnerService.DeleteAddressAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}