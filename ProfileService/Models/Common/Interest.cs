using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models.Common
{
    /// <summary>
    /// User interests
    /// </summary>
    public class Interest : BaseModel
    {
        /// <summary>
        /// Category
        /// </summary>
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public Guid? UploadId { get; set; }
        public Upload Upload { get; set; }
    }
}