using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Business.Address;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Business
{
    /// <summary>
    /// BusinessAddress controller
    /// </summary>
    [Route("api/business/addresses")]
    [AllowAnonymous]
    public class BusinessAddressController : BaseController
    {
        private readonly IBusinessService _businessService;
        private readonly ILogger<BusinessAddressController> _logger;

        public BusinessAddressController(IBusinessService businessService, ILogger<BusinessAddressController> logger)
        {
            _businessService = businessService;
            _logger = logger;
        }

        /// <summary>
        /// SEARCH business addresses
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetBusinessAddress>> Get(Guid businessId)
        {
            return await _businessService.GetAddressesAsync(businessId);
        }

        /// <summary>
        /// CREATE a business address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NewBusinessAddress> Create([FromBody] NewBusinessAddress address)
        {
            try
            {
                var result = await _businessService.AddAddressAsync(address);
                _logger.LogInformation(JsonConvert.SerializeObject(result));
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a business address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdateBusinessAddress> Update([FromBody] UpdateBusinessAddress address)
        {
            try
            {
                await _businessService.UpdateAddressAsync(address);
                return address;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// DELETE business address
        /// </summary>
        /// <param name="id"></param>
        /// <param name="businessId"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Delete(Guid businessId, Guid addressId)    
        {
            try
            {
                await _businessService.DeleteAddressAsync(businessId, addressId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}