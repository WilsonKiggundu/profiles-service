using System;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Contact
{
    /// <summary>
    /// Get a Contact
    /// </summary>
    public class GetContact : BaseModel
    {
        /// <summary>
        /// Type of contact
        /// </summary>
        /// <example>Email</example>
        public string Type { get; set; }
        
        /// <summary>
        /// Contact
        /// </summary>
        /// <example>someone@domain.com</example>
        public string Value { get; set; }
        
        /// <summary>
        /// Contact details
        /// </summary>
        /// <example>Personal</example>
        public string Details { get; set; }
        
        /// <summary>
        /// Owner of the contact
        /// </summary>
        public Guid BelongsTo { get; set; }
    }
}