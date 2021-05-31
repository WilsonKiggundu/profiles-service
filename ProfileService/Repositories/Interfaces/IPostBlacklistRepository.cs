using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Models.Posts;
using ProfileService.Models.Preferences;

namespace ProfileService.Repositories.Interfaces
{
    public interface IPostBlacklistRepository : IGenericRepository<PostBlacklist>
    {
        Task<ICollection<PostBlacklist>> GetByPersonIdAsync(Guid personId);  
        Task DeleteAsync(Guid personId, Guid postId);    
    }
}