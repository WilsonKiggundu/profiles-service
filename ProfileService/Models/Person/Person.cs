using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProfileService.Contracts.Person;
using ProfileService.Models.Common;

namespace ProfileService.Models.Person
{
    /// <summary>
    /// Personal profile
    /// </summary>
    public class Person : BaseModel
    {
        /// <summary>
        /// User Identifier
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [Required]
        public string Firstname { get; set; }
        
        /// <summary>
        /// Last name
        /// </summary>
        [Required]
        public string Lastname { get; set; }

        [Required]
        public string DateOfBirth { get; set; }

        [Required]
        public Gender? Gender { get; set; }

        /// <summary>
        /// Personal summary
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Profile photo
        /// </summary>
        public Guid? UploadId { get; set; }
        public Upload Upload { get; set; }

        /// <summary>
        /// Personal interests
        /// </summary>
        public IEnumerable<Interest> Interests { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<PersonCategory> Categories { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<PersonSkill> Skills { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<PersonAward> Awards { get; set; }
        
    }
}