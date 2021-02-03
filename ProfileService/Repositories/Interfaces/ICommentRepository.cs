using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Comment;
using ProfileService.Contracts.Common;
using ProfileService.Models.Posts;

namespace ProfileService.Repositories.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<SearchCommentsResponse> SearchAsync(SearchCommentsRequest filter);
    }
}