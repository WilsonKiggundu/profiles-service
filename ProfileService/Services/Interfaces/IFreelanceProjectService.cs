using System;
using System.Threading.Tasks;
using ProfileService.Contracts.FreelanceProject;
using ProfileService.Models;

namespace ProfileService.Services.Interfaces
{
    public interface IFreelanceProjectService : IService
    {
        Task<SearchFreelanceProjectResponse> SearchAsync(SearchFreelanceProjectRequest request);
        Task<FreelanceProject> GetByIdAsync(Guid id);
        Task<FreelanceProject> InsertAsync(FreelanceProject project);    
        Task<FreelanceProject> UpdateAsync(FreelanceProject project);
        Task DeleteAsync(Guid id);
    }
}