using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProfileService.Contracts.Person;
using ProfileService.Models.Common;

namespace ProfileService.Models.Person
{
    /// <summary>
    /// Personal profile
    /// </summary>
    public class Person : BaseModel
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        public string DateOfBirth { get; set; }

        [Required]
        public Gender? Gender { get; set; }
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
        
        [NotMapped] public int ConnectionsCount { get; set; }
        [NotMapped] public bool IsConnected { get; set; }
        
        [NotMapped] public string FullName { get; set; }

        public Person()
        {
            FullName = $"{Firstname} {Lastname}";
        }
        
    }
}