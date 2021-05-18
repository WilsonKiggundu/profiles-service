using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Contracts.FreelanceProject;
using ProfileService.Models;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class FreelanceProjectRepository : IFreelanceProjectRepository
    {
        private readonly ProfileServiceContext _context;

        public FreelanceProjectRepository(ProfileServiceContext context)
        {
            _context = context;
        }

        public IEnumerable<FreelanceProject> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<FreelanceProject> GetByIdAsync(Guid id)
        {
            return await _context
                .FreelanceProjects
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task InsertAsync(FreelanceProject project)
        {
            await _context.FreelanceProjects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task InsertManyAsync(ICollection<FreelanceProject> projects)
        {
            await _context.FreelanceProjects.AddRangeAsync(projects);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FreelanceProject project)
        {
            _context.FreelanceProjects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var toDelete = await _context.FreelanceProjects.FindAsync(id);
            _context.FreelanceProjects.Remove(toDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<SearchFreelanceProjectResponse> SearchAsync(SearchFreelanceProjectRequest request)
        {
            IQueryable<FreelanceProject> query = _context.FreelanceProjects;

            if (request.OwnerId.HasValue)
            {
                query = query.Where(q => q.OwnerId == request.OwnerId);
            }
            
            if (request.Status.HasValue)
            {
                query = query.Where(q => q.Status == request.Status);
            }
            
            if (request.HiredPersonId.HasValue)
            {
                query = query.Where(q => q.HiredPersonId == request.HiredPersonId);
            }
            
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(q => q.Name.ToLower().Contains(request.Name.ToLower()));
            }
            
            var skip = (request.Page - 1) * request.PageSize;
            var hasMore = await query.Skip(skip).CountAsync() > 0;

            var projects = await query.Skip(skip)
                .Take(request.PageSize)
                .ToListAsync();
            
            return new SearchFreelanceProjectResponse
            {
                HasMore = hasMore,
                Request = request,
                Projects = projects
            };
        }

        public async Task<ICollection<FreelanceProjectHire>> GetHiresAsync(Guid projectId)
        {
            return await _context.FreelanceProjectHires
                .Include(q => q.Person)
                .Include(q => q.Project)
                .ToListAsync();
        }

        public async Task AddHireAsync(FreelanceProjectHire hire)
        {
            await _context.FreelanceProjectHires.AddAsync(hire);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHireAsync(FreelanceProjectHire hire)
        {
            _context.FreelanceProjectHires.Update(hire);
            await _context.SaveChangesAsync();
        }
    }
}