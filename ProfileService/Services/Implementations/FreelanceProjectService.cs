using System;
using System.Threading.Tasks;
using ProfileService.Contracts.FreelanceProject;
using ProfileService.Models;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class FreelanceProjectService : IFreelanceProjectService
    {
        private readonly IFreelanceProjectRepository _repository;

        public FreelanceProjectService(IFreelanceProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<SearchFreelanceProjectResponse> SearchAsync(SearchFreelanceProjectRequest request)
        {
            return await _repository.SearchAsync(request);
        }

        public async Task<FreelanceProject> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<FreelanceProject> InsertAsync(FreelanceProject project)
        {
            project.Id = Guid.NewGuid();
            await _repository.InsertAsync(project);

            return project;
        }

        public async Task<FreelanceProject> UpdateAsync(FreelanceProject project)
        {
            await _repository.UpdateAsync(project);
            return project;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}