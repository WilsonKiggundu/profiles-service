using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Article;
using ProfileService.Models.Preferences;

namespace ProfileService.Services.Interfaces
{
    public interface IPostBlacklistService : IService
    {
        Task<IEnumerable<PostBlacklist>> GetByIdAsync(Guid personId);
        Task InsertAsync(PostBlacklist entity);          
        Task DeleteAsync(Guid personId, Guid postId);
    }
}