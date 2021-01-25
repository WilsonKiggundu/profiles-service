using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Lookup.Skill;
using ProfileService.Controllers.Common;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Lookup
{
    /// <summary>
    /// LookupSkill controller
    /// </summary>
    [Route("api/lookup/skills")]
    public class LookupSkillController : BaseController
    {
        private readonly ILookupSkillService _service;
        
        public LookupSkillController(ILookupSkillService service)
        {
            _service = service;
        }

        /// <summary>
        /// SEARCH lookup skill
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetLookupSkill>> Get()
        {
            return await _service.SearchAsync();
        }

        /// <summary>
        /// CREATE a lookup skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Create(NewLookupSkill skill)
        {
            try
            {
                await _service.InsertAsync(skill);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a lookup skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Update([FromBody] UpdateLookupSkill skill)
        {
            try
            {
                await _service.UpdateAsync(skill);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// DELETE lookup skill
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