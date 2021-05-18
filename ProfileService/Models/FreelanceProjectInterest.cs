using System;
using ProfileService.Models.Common;

namespace ProfileService.Models
{
    public class FreelanceProjectHire : BaseModel
    {
        public Guid ProjectId { get; set; }
        public FreelanceProject Project { get; set; }

        public Guid PersonId { get; set; }
        public Person.Person Person { get; set; }

        public HireStatus Status { get; set; }
    }

    public enum HireStatus
    {
        ExpressedInterest = 1,
        Rejected = 2,
        Considered = 3,
        Hired = 4
    }
}