using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Employee;
using ProfileService.Models.Employees;

namespace ProfileService.Repositories.Interfaces
{
    public interface IEmployeeWellnessRepository : IGenericRepository<EmployeeWellness>
    {
        Task<IEnumerable<EmployeeWellness>> SearchAsync(SearchEmployeeRequest request);
    }
}