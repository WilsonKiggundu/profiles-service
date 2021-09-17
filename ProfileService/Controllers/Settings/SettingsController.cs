using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Preferences;
using ProfileService.Controllers.Common;
using ProfileService.Models.Preferences;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Settings
{
    [Route("/api/settings")]
    public class SettingsController : BaseController
    {
        private readonly IPersonPreferenceService _service;
        private readonly IMapper _mapper;

        public SettingsController(IPersonPreferenceService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<EmailSettingsContract> Get(Guid personId)
        {
            var settings = await _service.GetByIdAsync(personId);
            return _mapper.Map<EmailSettingsContract>(settings);
        }

        [HttpPut]
        public async Task Update([FromBody] UpdatePreferenceContract preference)
        {
            await _service.UpdateAsync(preference);
        }
    }
}