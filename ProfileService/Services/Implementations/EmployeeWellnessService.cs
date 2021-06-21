using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Employee;
using ProfileService.Models.Employees;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class EmployeeWellnessService : IEmployeeWellnessService
    {
        private readonly IEmployeeWellnessRepository _repository;

        public EmployeeWellnessService(IEmployeeWellnessRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeWellness>> SearchAsync(SearchEmployeeRequest request)
        {
            return await _repository.SearchAsync(request);
        }

        public async Task<EmployeeWellness> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<EmployeeWellness> InsertAsync(EmployeeWellness wellness)
        {
            await _repository.AddOrUpdateAsync(wellness);
            return wellness;
        }

        public async Task<EmployeeWellness> UpdateAsync(EmployeeWellness employee)
        {
            await _repository.UpdateAsync(employee);
            return employee;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}