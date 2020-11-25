using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ProfileService.Models.Common
{
    /// <summary>
    /// Base model
    /// </summary>
    public abstract class BaseModel : IEntity
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Flag indicating if the record is deleted
        /// </summary>
        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Date and time when record was created
        /// </summary>
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Date and time when record was last updated
        /// </summary>
        public DateTime? DateLastUpdated { get; set; }
        
    }
}