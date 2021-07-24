using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Models.Audit;

namespace ProfileService.Services.Interfaces
{
    public interface ILogsService : IService
    {
        Task<IEnumerable<AuditLog>> SearchAsync(int page, int pageSize);
        Task<int> CountAsync(int page, int pageSize);   
    }
}