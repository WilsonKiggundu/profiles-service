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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ProfileServiceContext _context;

        public EmployeeRepository(ProfileServiceContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task InsertAsync(Employee entity)
        {
            await _context.Employees.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task InsertManyAsync(ICollection<Employee> entities)
        {
            await _context.Employees.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee entity)
        {
            _context.Employees.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var toRemove = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(toRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> SearchAsync(SearchEmployeeRequest request)
        {
            IQueryable<Employee> query = _context.Employees;

            if (request.Department.HasValue)
            {
                query = query.Where(q => q.Department == request.Department);
            }

            if (request.EmployeeId.HasValue)
            {
                query = query.Where(q => q.Id == request.EmployeeId);
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                var nameArray = request.Name.Split(" ");
            }

            return await query
                .OrderBy(q => q.Firstname)
                .ThenBy(q => q.Lastname)
                .ThenBy(q => q.Department)
                .ThenBy(q => q.Unit)
                .ThenBy(q => q.Position)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        }
    }
}