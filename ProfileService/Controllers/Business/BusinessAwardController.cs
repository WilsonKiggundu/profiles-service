using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Business.Product;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Business
{
    /// <summary>
    /// BusinessProduct controller
    /// </summary>
    [Route("api/business/awards")]
    public class BusinessAwardsController : BaseController
    {
        private readonly IBusinessService _businessService;

        public BusinessAwardsController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        /// <summary>
        /// SEARCH business products
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetBusinessProduct>> Get(Guid businessId)
        {
            return await _businessService.GetProductsAsync(businessId);
        }

        /// <summary>
        /// CREATE a business product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GetBusinessProduct> Create([FromBody] NewBusinessProduct product)
        {
            try
            {
                return await _businessService.AddProductAsync(product);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a business product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<GetBusinessProduct> Update([FromBody] UpdateBusinessProduct product)
        {
            try
            {
                return await _businessService.UpdateProductAsync(product);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE business product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Delete(Guid id)
        {
            try
            {
                await _businessService.DeleteProductAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}