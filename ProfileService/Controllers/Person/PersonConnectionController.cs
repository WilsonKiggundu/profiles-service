using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Person.Connections;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Person
{
    /// <summary>
    /// PersonConnection controller
    /// </summary>
    [Route("api/person/connections")]
    public class PersonConnectionController : BaseController
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonConnectionController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personService"></param>
        public PersonConnectionController(IPersonService personService, ILogger<PersonConnectionController> logger)
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
        public async Task<IEnumerable<GetPersonConnection>> Get(Guid personId)
        {
            return await _personService.GetConnectionsAsync(personId);
        }

        /// <summary>
        /// CREATE a person connection
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NewPersonConnection> Create([FromBody] NewPersonConnection connection)
        {
            try
            {
                _logger.LogInformation(JsonConvert.SerializeObject(connection, Formatting.Indented));
                return await _personService.AddConnectionAsync(connection);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a person connection
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdatePersonConnection> Update([FromBody] UpdatePersonConnection connection)
        {
            try
            {
                await _personService.UpdateConnectionAsync(connection);
                return connection;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE a person connection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _personService.DeleteConnectionAsync(id);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}