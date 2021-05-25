using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Job> GetJobAsync(Guid id)
        {
            return await _context.Jobs
                .Include(q => q.Profile)
                .Include(q => q.Company)
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}