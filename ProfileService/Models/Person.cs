using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models
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

        /// <summary>
        /// Personal summary
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Profile photo
        /// </summary>
        public string Photo { get; set; }

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