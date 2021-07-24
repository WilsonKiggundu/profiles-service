using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts;
using ProfileService.Contracts.Zoom;
using ProfileService.Controllers.Common;
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

        [HttpGet("{eventId}/participants/{type}")]
        public async Task<ICollection<WebinarParticipant>> GetParticipantsAsync(string eventId, string type)
        {
            return await _eventService.GetParticipantsAsync(eventId, type);
        }

        // [HttpPut("{eventId}")]
        // public async Task<EventContract> UpdateAsync(string eventId, [FromBody] EventContract eventContract)
        // {
        //     throw new NotImplementedException();
        // }
        //
        // [HttpDelete("{eventId}")]
        // public async Task DeleteAsync(string eventId)
        // {
        //     throw new NotImplementedException();
        // }
    }
}