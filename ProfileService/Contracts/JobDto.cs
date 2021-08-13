using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ProfileService.Contracts.Business;
using ProfileService.Contracts.Person;
using ProfileService.Models;
using ProfileService.Models.Common;

namespace ProfileService.Contracts
{
    public class JobDto
    {    
        public int Id { get; set; }
        public Guid? JobId { get; set; }
        
        [JsonProperty("deadline")]
        public string Deadline { get; set; }
        public Guid? ProfileId { get; set; }
        public Models.Person.Person Profile { get; set; }
        public string CompanyId { get; set; }
        public Models.Business.Business Company { get; set; }
        public string Details { get; set; }
        public string Experience { get; set; }
        public string Benefits { get; set; }
        public string Location { get; set; }
        public string Qualifications { get; set; }
        public string Title { get; set; }
        public string JobType { get; set; }
        public string MinSalary { get; set; }
        public string MaxSalary { get; set; }
        public string Skills { get; set; }
        public string ReplyEmail { get; set; }
        public JobCategory Category { get; set; }
        public ICollection<JobApplicant> Applicants { get; set; }
        public ICollection<JobApplicantProfile> Applications { get; set; }
        public List<JobUpload> Uploads { get; set; }
    }

    public class JobCategory
    {
        public int Id { get; set; }    
        public string Name { get; set; }
    }

    public class JobUpload
    {
        public int EntityId { get; set; }
        public int Id { get; set; }
        public string EntityType { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }

    public class JobSearch
    {
        public int? Id { get; set; }    
        public Guid? JobId { get; set; }
        public Guid? ProfileId { get; set; }
        public Guid? CompanyId { get; set; }    
        public string CompanyName { get; set; }
    }

    public class JobApplicantProfile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public Guid JobId { get; set; }
        public Guid ApplicationId { get; set; }
        public string DateTime { get; set; }
        public HireStatus Status { get; set; }
    }

    public class JobApplicant
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public string ProfileId { get; set; }
        public string DateTime { get; set; }
        public string NotifyEmail { get; set; }    
        public string Status { get; set; }
    }    
}