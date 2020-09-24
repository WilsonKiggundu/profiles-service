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
        public ICollection<Interest> Interests { get; set; }
        
    }
}