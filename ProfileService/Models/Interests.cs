using System;

namespace ProfileService.Models
{
    /// <summary>
    /// User interests
    /// </summary>
    public class Interests : BaseModel
    {
        /// <summary>
        /// Category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public Guid? Icon { get; set; }
    }
}