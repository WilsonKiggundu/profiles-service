using System;

namespace ProfileService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonInterest : BaseModel
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
        public Guid InterestId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Interest Type { get; set; }
    }
}