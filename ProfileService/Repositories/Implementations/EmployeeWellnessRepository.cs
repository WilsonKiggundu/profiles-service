using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Contracts.Employee;
using ProfileService.Models.Employees;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class EmployeeWellnessRepository : IEmployeeWellnessRepository
    {
        private readonly ProfileServiceContext _context;

        public EmployeeWellnessRepository(ProfileServiceContext context)
        {
            _context = context;
        }

        public IEnumerable<EmployeeWellness> GetAll()
        {
            return _context.EmployeeWellness.ToList();
        }

        public async Task<EmployeeWellness> GetByIdAsync(Guid id)
        {
            return await _context.EmployeeWellness.FindAsync(id);
        }

        public async Task InsertAsync(EmployeeWellness entity)
        {
            await _context.EmployeeWellness.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task InsertManyAsync(ICollection<EmployeeWellness> entities)
        {
            await _context.EmployeeWellness.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmployeeWellness entity)
        {
            _context.EmployeeWellness.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var toRemove = await _context.EmployeeWellness.FindAsync(id);
            _context.EmployeeWellness.Remove(toRemove);
            await _context.SaveChangesAsync();
        }

        public async Task AddOrUpdateAsync(EmployeeWellness wellness)
        {
            var exists = await _context.EmployeeWellness
                .Where(q => q.ReportingDate.Year == DateTime.Today.Year)
                .Where(q => q.ReportingDate.Month == DateTime.Today.Month)
                .Where(q => q.ReportingDate.Day == DateTime.Today.Day)
                .FirstOrDefaultAsync(q => q.EmployeeId == wellness.EmployeeId);

            if (exists != null)
            {
                exists.Status = wellness.Status;
                exists.Details = wellness.Details;
                exists.IllnessType = wellness.IllnessType;
                _context.EmployeeWellness.Update(exists);
            }
            else
            {
                await _context.EmployeeWellness.AddAsync(wellness);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeWellness>> SearchAsync(SearchEmployeeRequest request)
        {
            IQueryable<EmployeeWellness> query = _context.EmployeeWellness
                .Where(q => q.ReportingDate.Year == DateTime.Today.Year)
                .Where(q => q.ReportingDate.Month == DateTime.Today.Month)
                .Where(q => q.ReportingDate.Day == DateTime.Today.Day)
                .Include(q => q.Employee);

            if (request.Status.HasValue)
            {
                query = query.Where(q => q.Status == request.Status);
            }

            if (request.EmployeeId.HasValue)
            {
                query = query.Where(q => q.Id == request.EmployeeId);
            }

            return await query.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
        }
    }
}