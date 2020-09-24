using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models
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
        public string Icon { get; set; }
    }
}