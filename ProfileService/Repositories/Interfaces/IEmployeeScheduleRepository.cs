using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Employee;
using ProfileService.Models.Employees;

namespace ProfileService.Repositories.Interfaces
{
    public interface IEmployeeScheduleRepository : IGenericRepository<EmployeeSchedule>
    {    
        Task<IEnumerable<EmployeeSchedule>> SearchAsync(SearchEmployeeRequest request);
    }
}