using System;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Contracts.FreelanceProject;
using ProfileService.Models;
using ProfileService.Models.Posts;

namespace ProfileService.Repositories.Interfaces
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        Task<Job> GetJobAsync(Guid id);
    }
}