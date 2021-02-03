using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProfileService.Models.Common;

namespace ProfileService.Models.Business
{
    public class Business : BaseModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string EmployeeCount { get; set; }
        
        public string IncorporationDate { get; set; }

        public string Website { get; set; }

        public string CoverPhoto { get; set; }
        public string Avatar { get; set; }

        public ICollection<BusinessInterest> Interests { get; set; }
        public ICollection<BusinessAddress> Addresses { get; set; }
        public ICollection<BusinessProduct> Products { get; set; }
        public ICollection<BusinessContact> Contacts { get; set; }
        public ICollection<BusinessNeed> Needs { get; set; }
        public ICollection<BusinessRole> Roles { get; set; }

        [NotMapped] public bool ProfileComplete { get; set; }

        public Business()
        {
            ProfileComplete = !string.IsNullOrEmpty(Description)
                              && !string.IsNullOrEmpty(Website)
                              && !string.IsNullOrEmpty(EmployeeCount);
        }
    }

    public enum BusinessCategory
    {
        Fintech = 1,
        EdTech = 2,
        AgriTech = 3,   
        LegalTech = 4,
        Other = 99
    }
}