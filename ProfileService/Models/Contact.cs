using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Contact : BaseModel
    {
        /// <summary>
        /// Contact type
        /// </summary>
        [Required]
        public ContactType Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// Person or business to whom contact belongs
        /// </summary>
        [Required]
        public Guid BelongsTo { get; set; }

        /// <summary>
        /// Contact details
        /// </summary>
        public string Details { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ContactType
    {
        /// <summary>
        /// Telephone
        /// </summary>
        Telephone = 1,
        
        /// <summary>
        /// Email address
        /// </summary>
        Email = 2,
    }
}