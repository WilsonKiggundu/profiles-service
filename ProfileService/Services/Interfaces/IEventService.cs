using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts;
using ProfileService.Contracts.Zoom;

namespace ProfileService.Services.Interfaces
{
    public interface IEventService : IService
    {
        Task<ICollection<EventContract>> SearchAsync(EventSearch search);  
        Task<ICollection<Participant>> GetParticipantsAsync(EventType type, string eventId);
        Task<ICollection<Registrant>> GetRegistrantsAsync(EventType type, string eventId);  
        Task<EventContract> CreateAsync(EventContract eventContract);
        Task RegisterAsync(string eventId, Guid personId);
        Task<EventContract> UpdateAsync(EventContract eventContract);
        Task<EventContract> GetByIdAsync(string eventId);
        Task DeleteAsync(string eventId);       
    }
}