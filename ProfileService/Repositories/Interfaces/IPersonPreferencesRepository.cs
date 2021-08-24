using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Preferences;
using ProfileService.Models.Preferences;

namespace ProfileService.Repositories.Interfaces
{
    public interface IPersonPreferencesRepository : IGenericRepository<EmailSettings>
    {
        Task<bool> ExistsAsync(Guid personId);
    }
}