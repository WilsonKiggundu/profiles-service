using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Models.Posts;

namespace ProfileService.Services.Interfaces
{
    public interface IPostService : IService    
    {    
        Task<SearchPostResponse> SearchAsync(SearchPostRequest request);
        Task<NewPost> InsertAsync(NewPost post);
    }
}