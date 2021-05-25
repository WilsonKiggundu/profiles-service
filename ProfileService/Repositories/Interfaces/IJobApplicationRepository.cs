using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Contracts.FreelanceProject;
using ProfileService.Models;
using ProfileService.Models.Posts;

namespace ProfileService.Repositories.Interfaces
{
    public interface IJobApplicationRepository : IGenericRepository<JobApplication>
    {
        Task<ICollection<JobApplication>> SearchAsync(int jobId);
        Task<Job> GetJobById(int jobId);
    }
}