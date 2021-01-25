using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Person.Interests;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Person
{
    /// <summary>
    /// PersonInterest controller
    /// </summary>
    [Route("api/person/interests")]
    public class PersonInterestController : BaseController
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonInterestController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personService"></param>
        public PersonInterestController(IPersonService personService, ILogger<PersonInterestController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        /// <summary>
        /// SEARCH persons
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetPersonInterest>> Get(Guid personId)
        {
            return await _personService.GetInterestsAsync(personId);
        }

        /// <summary>
        /// CREATE a person interest
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NewPersonInterest> Create(NewPersonInterest model)
        {
            try
            {
                _logger.LogInformation(JsonConvert.SerializeObject(model, Formatting.Indented));
                return await _personService.AddInterestAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a person interest
        /// </summary>
        /// <param name="interest"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdatePersonInterest> Update([FromBody] UpdatePersonInterest interest)
        {
            try
            {
                await _personService.UpdateInterestAsync(interest);
                return interest;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// DELETE a person interest
        /// </summary>
        /// <param name="interestId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid interestId, Guid personId)
        {
            try
            {
                await _personService.DeleteInterestAsync(interestId, personId);
                return Ok(new {interestId, personId});
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}