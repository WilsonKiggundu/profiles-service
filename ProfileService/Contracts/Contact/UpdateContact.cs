using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Contracts.Business
{
    /// <summary>
    /// Update Business
    /// </summary>
    public class UpdateContact
    {
        /// <summary>
        /// Business Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Type of contact
        /// </summary>
        /// <example>Email</example>
        [Required]
        public string Type { get; set; }
        
        /// <summary>
        /// Business
        /// </summary>
        /// <example>someone@domain.com</example>
        [Required]
        public string Value { get; set; }
        
        /// <summary>
        /// Business details
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