using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts;

namespace ProfileService.Services.Interfaces
{
    public interface IJobService : IService
    {
        Task<ICollection<JobDto>> GetAsync(JobSearch search);
        Task<JobDto> CreateAsync(JobDto jobDto);
    }
}