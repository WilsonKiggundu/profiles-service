using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Admin.Models.Person
{
    public class Person : BaseModel
    {
        public Guid UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Bio { get; set; }
        public string Avatar { get; set; }    
        public string CoverPhoto { get; set; }
        
        public virtual IEnumerable<PersonInterest> Interests { get; set; }
        public virtual IEnumerable<PersonCategory> Categories { get; set; }
        public virtual IEnumerable<PersonSkill> Skills { get; set; }
        public virtual IEnumerable<PersonContact> Contacts { get; set; }
        public virtual IEnumerable<PersonAward> Awards { get; set; } 
        public virtual IEnumerable<PersonConnection> Connections { get; set; }
        public virtual IEnumerable<PersonStack> Stacks { get; set; }
        public virtual IEnumerable<PersonEmployment> Employment { get; set; }
        public virtual IEnumerable<PersonProject> Projects { get; set; }    
        public virtual FreelanceTerms FreelanceTerms { get; set; }
        
    }
}