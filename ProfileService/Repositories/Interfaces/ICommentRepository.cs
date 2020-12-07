using System;
using System.Collections.Generic;
using ProfileService.Models.Posts;

namespace ProfileService.Repositories.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        IEnumerable<Comment> GetAll(Guid? postId = null, Guid? articleId = null);
    }
}