using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Article;
using ProfileService.Models.Preferences;

namespace ProfileService.Services.Interfaces
{
    public interface IPersonBlacklistService : IService
    {
        Task<IEnumerable<PersonBlacklist>> GetByIdAsync(Guid personId);
        Task InsertAsync(PersonBlacklist entity);
        Task DeleteAsync(Guid personId, Guid blacklistId);
    }
}