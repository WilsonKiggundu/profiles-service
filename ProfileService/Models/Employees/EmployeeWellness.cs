using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProfileService.Models.Common;

namespace ProfileService.Models.Employees
{
    [Table("Wellness", Schema = "employees")]
    public class EmployeeWellness : BaseModel
    {
        public DateTime ReportingDate { get; set; } = DateTime.UtcNow;
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public Wellness Status { get; set; } = Wellness.FitToWork;
        public IllnessType? IllnessType { get; set; }
        public string Details { get; set; }
    }

    public enum IllnessType
    {
        PositiveCovidTest = 1,
        Other = 99
    }

    public enum Wellness
    {
        [Display(Name = "Fit to work")]
        FitToWork = 1,
        
        [Display(Name = "Un well")]
        Unwell = 2,
        
        [Display(Name = "In isolation")]
        InIsolation = 3,
        
        [Display(Name = "Nursing a family member")]
        NursingFamilyMember = 4,
        
        [Display(Name = "Other")]
        Other = 99
    }
}