using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Models.Posts;

namespace ProfileService.Repositories.Interfaces
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        IEnumerable<Post> GetPostsByAuthorId(Guid authorId);
    }
}