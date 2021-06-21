using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Employee;
using ProfileService.Models.Employees;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Employee>> SearchAsync(SearchEmployeeRequest request)
        {
            return await _repository.SearchAsync(request);
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Employee> InsertAsync(Employee employee)
        {
            await _repository.InsertAsync(employee);
            return employee;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            await _repository.UpdateAsync(employee);
            return employee;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<Dashboard> GetDashboardAsync()
        {
            return await _repository.GetDashboardAsync();
        }
    }
}