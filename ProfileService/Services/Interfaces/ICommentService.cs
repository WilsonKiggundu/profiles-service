using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Comment;

namespace ProfileService.Services.Interfaces
{
    public interface ICommentService : IService
    {
        IEnumerable<GetComment> GetAll();
        IEnumerable<GetComment> GetAll(Guid? postId, Guid? articleId);
        Task<GetComment> GetByIdAsync(Guid id);
        Task InsertAsync(NewComment comment);    
        Task UpdateAsync(UpdateComment comment);
        Task DeleteAsync(Guid id);
    }
}