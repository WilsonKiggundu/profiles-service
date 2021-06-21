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
    public class EmployeeScheduleRepository : IEmployeeScheduleRepository
    {
        private readonly ProfileServiceContext _context;

        public EmployeeScheduleRepository(ProfileServiceContext context)
        {
            _context = context;
        }

        public IEnumerable<EmployeeSchedule> GetAll()
        {
            return _context.EmployeeSchedules.ToList();
        }

        public async Task<EmployeeSchedule> GetByIdAsync(Guid id)
        {
            return await _context.EmployeeSchedules.FindAsync(id);
        }

        public async Task InsertAsync(EmployeeSchedule entity)
        {
            await _context.EmployeeSchedules.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task InsertManyAsync(ICollection<EmployeeSchedule> entities)
        {
            await _context.EmployeeSchedules.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmployeeSchedule entity)
        {
            _context.EmployeeSchedules.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var toRemove = await _context.EmployeeSchedules.FindAsync(id);
            _context.EmployeeSchedules.Remove(toRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeSchedule>> SearchAsync(SearchEmployeeRequest request)
        {
            IQueryable<EmployeeSchedule> query = _context
                .EmployeeSchedules
                .Include(q => q.Employee);
            
            if (request.EmployeeId.HasValue)
            {
                query = query.Where(q => q.Id == request.EmployeeId);
            }

            return await query.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
        }
    }
}