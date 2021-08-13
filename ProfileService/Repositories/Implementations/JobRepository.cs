using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Contracts;
using ProfileService.Models;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class JobRepository : IJobRepository
    {
        private readonly ProfileServiceContext _context;

        public JobRepository(ProfileServiceContext context)
        {
            _context = context;
        }

        public IEnumerable<Job> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Job> GetByIdAsync(Guid id)
        {
            return await _context.Jobs.FindAsync(id);
        }

        public async Task InsertAsync(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
        }

        public async Task InsertManyAsync(ICollection<Job> entities)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Job job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var job = await _context.Jobs.FindAsync(id);
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Job>> GetManyAsync(List<int> ids)
        {
            return await _context.Jobs
                .Include(q => q.Profile)
                .Include(q => q.Company)
                .Where(q => ids.Contains(q.Reference))
                .ToListAsync();
        }

        public async Task<Job> GetJobAsync(Guid id)
        {
            return await _context.Jobs
                .Include(q => q.Profile)
                .Include(q => q.Company)
                .Include(q => q.Applications)
                .ThenInclude(a => a.Applicant)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<ICollection<JobApplicantProfile>> GetApplicantsAsync(Guid? id)
        {
            IQueryable<JobApplication> query = _context.JobApplications
                .Include(q => q.Applicant);

            if (id.HasValue)
            {
                query = query.Where(q => q.JobId == id);
            }
            
            return await query.Select(s => new JobApplicantProfile
                {
                    Id = s.ApplicantId,
                    Name = $"{s.Applicant.Firstname} {s.Applicant.Lastname}",
                    Email = s.Applicant.Email,
                    Status = s.Status,
                    DateTime = s.DateCreated,
                    ApplicationId = s.Id,
                    JobId = s.JobId,
                    LastUpdated = s.DateLastUpdated
                })
                .ToListAsync();
        }

        public async Task<Job> GetJobAsync(int id)
        {
            return await _context.Jobs
                .Include(q => q.Profile)
                .Include(q => q.Company)
                .Include(q => q.Applications)
                .ThenInclude(a => a.Applicant)
                .FirstOrDefaultAsync(q => q.Reference == id);
        }
    }
}