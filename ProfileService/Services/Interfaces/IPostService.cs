using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Models.Common;
using ProfileService.Models.Posts;

namespace ProfileService.Services.Interfaces
{
    public interface IPostService : IService    
    {    
        Task<SearchPostResponse> SearchAsync(SearchPostRequest request);
        Task<Post> InsertAsync(Post post);
        Task UpdateAsync(UpdatePost post);
        Task DeleteAsync(Guid id);
    }
}