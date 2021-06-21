using System;
using System.ComponentModel.DataAnnotations.Schema;
using ProfileService.Models.Common;

namespace ProfileService.Models.Employees
{
    [Table("Wellness", Schema = "employees")]
    public class EmployeeWellness : BaseModel
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; } = DateTime.UtcNow.AddDays(7);

        public Wellness Status { get; set; } = Wellness.FitToWork;
    }

    public enum Wellness
    {
        FitToWork = 1,
        Unwell = 2,
        InIsolation = 3,
        Other = 99
    }
}