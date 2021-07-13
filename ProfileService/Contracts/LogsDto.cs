using System.Collections.Generic;
using ProfileService.Models.Audit;

namespace ProfileService.Contracts
{
    public class LogsDto
    {
        public int Total { get; set; }
        public IEnumerable<AuditLog> Logs { get; set; }
    }
}