using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Models.Audit;
using ProfileService.Repositories;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class LogsService : ILogsService
    {
        private readonly ProfileServiceContext _context;

        public LogsService(ProfileServiceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuditLog>> SearchAsync(int page, int pageSize)
        {
            return await _context.AuditLogs.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> CountAsync(int page, int pageSize)
        {
            return await _context.AuditLogs.Skip((page - 1) * pageSize).CountAsync();
        }
    }
}