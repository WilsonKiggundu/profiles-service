using System;
using System.ComponentModel.DataAnnotations.Schema;
using ProfileService.Models.Common;

namespace ProfileService.Models.Employees
{
    [Table("Schedules", Schema = "employees")]
    public class EmployeeSchedule : BaseModel
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; } = DateTime.UtcNow.AddDays(7);

        public WorkStation WorkStation { get; set; } = WorkStation.Remote;
    }

    public enum WorkStation
    {
        Remote = 1,
        Office = 2,
        AnnualLeave = 3,
    }
}