using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Post;

namespace ProfileService.Services.Interfaces
{
    public interface IPostService : IService
    {
        IEnumerable<GetPost> GetAll();
        Task<GetPost> GetByIdAsync(Guid id);
        Task InsertAsync(NewPost post);
        Task UpdateAsync(UpdatePost post);
        Task DeleteAsync(Guid id);
    }
}