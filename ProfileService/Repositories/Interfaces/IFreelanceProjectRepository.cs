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
    }
}