using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Employee;
using ProfileService.Models.Employees;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class EmployeeScheduleService : IEmployeeScheduleService
    {
        private readonly IEmployeeScheduleRepository _repository;

        public EmployeeScheduleService(IEmployeeScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeSchedule>> SearchAsync(SearchEmployeeRequest request)
        {
            return await _repository.SearchAsync(request);
        }

        public async Task<EmployeeSchedule> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<EmployeeSchedule> InsertAsync(EmployeeSchedule employee)
        {
            employee.Id = Guid.NewGuid();
            await _repository.InsertAsync(employee);
            return employee;
        }

        public async Task<EmployeeSchedule> UpdateAsync(EmployeeSchedule employee)
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