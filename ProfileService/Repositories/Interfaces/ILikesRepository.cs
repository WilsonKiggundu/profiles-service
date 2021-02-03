using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Models.Posts;

namespace ProfileService.Repositories.Interfaces
{
    public interface ILikesRepository : IGenericRepository<Like>
    {
        Task<SearchLikeResponse> SearchAsync(SearchLikeRequest request);
    }
}