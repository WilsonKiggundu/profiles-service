#nullable enable
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Person;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Person
{
    /// <summary>
    /// Person controller
    /// </summary>
    [Route("api/person")]
    public class PersonController : BaseController
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personService"></param>
        /// <param name="logger"></param>
        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        /// <summary>
        /// SEARCH persons
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<SearchPersonResponse> Get([FromQuery] SearchPersonRequest request)
        {
            return await _personService.SearchAsync(request);
        }
        
        /// <summary>
        /// GET person by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var profile = await _personService.GetByIdAsync(id);
            
            _logger.LogInformation(JsonConvert.SerializeObject(profile, Formatting.Indented));

            return Ok(profile);
        }

        /// <summary>
        /// CREATE a person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NewPerson> Create(NewPerson person)
        {
            try
            {
                return await _personService.InsertAsync(person);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        [HttpPut("coverPhoto")]
        public async Task<UpdatePerson> UpdateCoverPhoto(UpdatePerson person)
        {
            try
            {
                return await _personService.UpdateCoverPhotoAsync(person);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        [HttpPut("avatar")]
        public async Task<UpdatePerson> UpdateAvatar(UpdatePerson person)
        {
            try
            {
                return await _personService.UpdateAvatarAsync(person);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdatePerson> Update(UpdatePerson person)
        {
            try
            {
                return await _personService.UpdateAsync(person);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _personService.DeleteAsync(id);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}