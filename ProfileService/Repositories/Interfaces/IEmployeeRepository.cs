using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Employee;
using ProfileService.Models.Employees;

namespace ProfileService.Repositories.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> SearchAsync(SearchEmployeeRequest request);
        Task<Dashboard> GetDashboardAsync();
    }
}