using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Comment;

namespace ProfileService.Services.Interfaces
{
    public interface ICommentService : IService
    {    
        Task<SearchCommentsResponse> SearchAsync(SearchCommentsRequest filter);
        Task<NewComment> InsertAsync(NewComment comment);    
        Task<UpdateComment> UpdateAsync(UpdateComment comment);
        Task DeleteAsync(Guid id);
    }
}