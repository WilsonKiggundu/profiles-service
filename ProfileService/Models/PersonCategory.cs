using System;

namespace ProfileService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonCategory : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid PersonId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Category { get; set; }
    }
}