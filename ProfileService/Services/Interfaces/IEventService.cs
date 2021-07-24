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
        Task<ICollection<WebinarParticipant>> GetParticipantsAsync(string eventId, string type);
        Task<EventContract> CreateAsync(EventContract eventContract);
        Task<EventContract> GetByIdAsync(string eventId);
        Task DeleteAsync(string eventId);       
    }
}