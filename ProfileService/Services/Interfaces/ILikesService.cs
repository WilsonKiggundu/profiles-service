using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Models.Posts;

namespace ProfileService.Services.Interfaces
{
    public interface ILikesService : IService        
    {        
        Task<SearchLikeResponse> SearchAsync(SearchLikeRequest request);
        Task<Like> InsertAsync(Like like);
    }
}