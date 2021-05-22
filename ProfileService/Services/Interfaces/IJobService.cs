using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts;

namespace ProfileService.Services.Interfaces
{
    public interface IJobService : IService
    {
        Task<ICollection<Job>> GetAsync(JobSearch search);
        Task<Job> CreateAsync(Job job);
    }
}