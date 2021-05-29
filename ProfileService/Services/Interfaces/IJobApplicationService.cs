using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.FreelanceProject;
using ProfileService.Models;

namespace ProfileService.Services.Interfaces
{
    public interface IJobApplicationService : IService    
    {    
        Task<ICollection<JobApplication>> SearchAsync(Guid jobId);
        Task<JobApplication> InsertAsync(JobApplication application);    
        Task<JobApplication> UpdateAsync(JobApplication application);
        Task DeleteAsync(Guid id);
    }
}