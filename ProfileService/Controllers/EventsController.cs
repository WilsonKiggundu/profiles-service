using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts;
using ProfileService.Contracts.Zoom;
using ProfileService.Controllers.Common;
using ProfileService.Helpers;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers
{
    [Route("/api/events")]
    public class EventsController : BaseController
    {
        private readonly IEventService _eventService;
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ICollection<EventContract>> SearchAsync([FromQuery] EventSearch search)
        {
            return await _eventService.SearchAsync(search);
        }

        [HttpGet("{eventId}")]
        public async Task<EventContract> GetByIdAsync(string eventId)
        {
            return await _eventService.GetByIdAsync(eventId);
        }

        [HttpPost]
        public async Task<EventContract> CreateAsync([FromBody] EventContract eventContract)
        {
            return await _eventService.CreateAsync(eventContract);
        }

        [HttpPost("{eventId}/register/{personId}")]
        public async Task RegisterAsync(string eventId, Guid personId)
        {
            await _eventService.RegisterAsync(eventId, personId);  
        }

        [HttpPut]
        public async Task<EventContract> UpdateAsync([FromBody] EventContract eventContract)
        {
            return await _eventService.UpdateAsync(eventContract);
        }
        
        [HttpDelete]
        public async Task DeleteAsync(string eventId)
        {
            await _eventService.DeleteAsync(eventId);
        }
        
        [HttpGet("participants")]
        public async Task<ICollection<Participant>> GetParticipantsAsync(EventType type, string eventId)
        {
            return await _eventService.GetParticipantsAsync(type, eventId);
        }

        [HttpGet("registrants")]
        public async Task<ICollection<Registrant>> GetRegistrantsAsync(EventType type, string eventId)
        {
            return await _eventService.GetRegistrantsAsync(type, eventId);
        }
    }
}