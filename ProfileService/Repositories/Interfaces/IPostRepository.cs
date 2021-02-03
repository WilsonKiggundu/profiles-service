using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Models.Posts;

namespace ProfileService.Repositories.Interfaces
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<SearchPostResponse> SearchAsync(SearchPostRequest request);

    }
}