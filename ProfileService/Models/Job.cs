using System;
using System.Collections.Generic;
using ProfileService.Models.Common;

namespace ProfileService.Models
{
    public class Job : BaseModel
    {    
        public int Reference { get; set; }
        public string Title { get; set; }
        public string ReplyEmail { get; set; }
        public Guid? ProfileId { get; set; }
        public Person.Person Profile { get; set; }

        public Guid? CompanyId { get; set; }    
        public Business.Business Company { get; set; }
        
        public ICollection<JobApplication> Applications { get; set; }
    }
}