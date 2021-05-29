using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.FreelanceProject;
using ProfileService.Models;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly ProfileServiceContext _context;
        private readonly ILogger<JobApplicationRepository> _logger;

        public JobApplicationRepository(ProfileServiceContext context, ILogger<JobApplicationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<JobApplication> GetAll()
        {
            return _context.JobApplications.ToList();
        }

        public async Task<JobApplication> GetByIdAsync(Guid id)
        {
            return await _context
                .JobApplications
                .Include(p => p.Applicant)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task InsertAsync(JobApplication application)
        {
            await _context.JobApplications.AddAsync(application);
            await _context.SaveChangesAsync();
        }

        public async Task InsertManyAsync(ICollection<JobApplication> applications)
        {
            await _context.JobApplications.AddRangeAsync(applications);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(JobApplication application)
        {
            _context.JobApplications.Update(application);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var toDelete = await _context.JobApplications.FindAsync(id);
            _context.JobApplications.Remove(toDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<JobApplication>> SearchAsync(Guid jobId)
        {
            return await _context
                .JobApplications
                .Include(p => p.Applicant)
                .OrderByDescending(q => q.DateCreated)
                .Where(q => q.JobId == jobId)
                .ToListAsync();
        }
    }
}