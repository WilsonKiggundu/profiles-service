using System;
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
        public Guid Photo { get; set; }
        
    }
}