using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Contracts.FreelanceProject;
using ProfileService.Models;
using ProfileService.Models.Posts;

namespace ProfileService.Repositories.Interfaces
{
    public interface IFreelanceProjectRepository : IGenericRepository<FreelanceProject>
    {
        Task<SearchFreelanceProjectResponse> SearchAsync(SearchFreelanceProjectRequest request);
        Task<ICollection<FreelanceProjectHire>> GetHiresAsync(Guid projectId);
        Task AddHireAsync(FreelanceProjectHire hire);    
        Task UpdateHireAsync(FreelanceProjectHire hire);
    }
}