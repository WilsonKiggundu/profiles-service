using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models.Common
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

        /// <summary>
        /// Contact category
        /// </summary>
        public ContactCategory Category { get; set; } = ContactCategory.Primary;
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ContactType
    {
        Telephone = 1,
        Email = 2,
        Other = 99
    }

    public enum ContactCategory
    {
        Primary = 1,
        Alternative = 2,
        Other = 99
    }
}