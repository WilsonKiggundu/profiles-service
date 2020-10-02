using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Person
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonAward : BaseModel
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
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AwardedBy { get; set; }
        
    }
}