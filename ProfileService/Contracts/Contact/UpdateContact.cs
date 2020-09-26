using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Contracts.Contact
{
    /// <summary>
    /// Update Contact
    /// </summary>
    public class UpdateContact
    {
        /// <summary>
        /// Contact Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Type of contact
        /// </summary>
        /// <example>Email</example>
        [Required]
        public string Type { get; set; }
        
        /// <summary>
        /// Contact
        /// </summary>
        /// <example>someone@domain.com</example>
        [Required]
        public string Value { get; set; }
        
        /// <summary>
        /// Contact details
        /// </summary>
        /// <example>Personal</example>
        public string Details { get; set; }
        
        /// <summary>
        /// Owner of the contact
        /// </summary>
        [Required]
        public Guid BelongsTo { get; set; }
        
    }
}