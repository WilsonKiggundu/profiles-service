using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Models.Posts;
using ProfileService.Models.Preferences;

namespace ProfileService.Repositories.Interfaces
{
    public interface IPersonBlacklistRepository : IGenericRepository<PersonBlacklist>
    {
        Task<ICollection<PersonBlacklist>> GetByPersonIdAsync(Guid personId);   
        Task DeleteAsync(Guid personId, Guid blacklistId);        
    }
}