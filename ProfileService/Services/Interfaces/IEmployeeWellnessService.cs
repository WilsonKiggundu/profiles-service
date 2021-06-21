using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Business;
using ProfileService.Contracts.Employee;
using ProfileService.Models.Employees;

namespace ProfileService.Services.Interfaces
{
    public interface IEmployeeWellnessService : IService    
    {    
        Task<IEnumerable<EmployeeWellness>> SearchAsync(SearchEmployeeRequest request);
        Task<EmployeeWellness> GetByIdAsync(Guid id);
        Task<EmployeeWellness> InsertAsync(EmployeeWellness wellness);    
        Task<EmployeeWellness> UpdateAsync(EmployeeWellness wellness);
        Task DeleteAsync(Guid id);
    }
}