using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProfileService.Models.Common;

namespace ProfileService.Models
{
    public class JobApplication : BaseModel
    {
        public int JobId { get; set; }
        public Guid ApplicantId { get; set; }
        public Person.Person Applicant { get; set; }
        public HireStatus Status { get; set; } = HireStatus.ExpressedInterest;
        public string Remarks { get; set; }
        
        [NotMapped]
        public Job Job { get; set; }
    }
}