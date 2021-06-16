using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Business;
using ProfileService.Contracts.Employee;
using ProfileService.Models.Employees;

namespace ProfileService.Services.Interfaces
{
    public interface IEmployeeScheduleService : IService    
    {    
        Task<IEnumerable<EmployeeSchedule>> SearchAsync(SearchEmployeeRequest request);
        Task<EmployeeSchedule> GetByIdAsync(Guid id);
        Task<EmployeeSchedule> InsertAsync(EmployeeSchedule wellness);    
        Task<EmployeeSchedule> UpdateAsync(EmployeeSchedule wellness);
        Task DeleteAsync(Guid id);
    }
}