using System;
using System.ComponentModel.DataAnnotations.Schema;
using ProfileService.Models.Common;

namespace ProfileService.Models.Audit
{
    [Table("AuditLogs", Schema = "audit")]
    public class AuditLog
    {
        public int Id { get; set; }
        public DateTime InsertedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
        public string Data { get; set; }
        public string EventType { get; set; }
        public string User { get; set; }
    }
}