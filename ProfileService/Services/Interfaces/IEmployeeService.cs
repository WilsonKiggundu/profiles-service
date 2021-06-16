using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Business;
using ProfileService.Contracts.Employee;
using ProfileService.Models.Employees;

namespace ProfileService.Services.Interfaces
{
    public interface IEmployeeService : IService    
    {
        Task<IEnumerable<Employee>> SearchAsync(SearchEmployeeRequest request);
        Task<Employee> GetByIdAsync(Guid id);
        Task<Employee> InsertAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task DeleteAsync(Guid id);
    }
}