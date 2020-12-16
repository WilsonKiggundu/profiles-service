using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProfileService.Models.Common;

namespace ProfileService.Models.Business
{
    public class Business : BaseModel
    {
        /// <summary>
        /// Business name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// About the business
        /// </summary>
        public string Description { get; set; }

        public string Category { get; set; }

        /// <summary>
        /// Number of employees
        /// </summary>
        public int? EmployeeCount { get; set; }
        
        /// <summary>
        /// Incorporation date
        /// </summary>
        public string IncorporationDate { get; set; }

        /// <summary>
        /// Website URL
        /// </summary>
        public string Website { get; set; }

        public string CoverPhoto { get; set; }
        public string Avatar { get; set; }    

        [NotMapped] public bool ProfileComplete { get; set; }

        public Business()
        {
            ProfileComplete = !string.IsNullOrEmpty(Description)
                              && !string.IsNullOrEmpty(Website)
                              && EmployeeCount.HasValue;
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