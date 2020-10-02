using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Person
{
    /// <summary>
    /// Person skill
    /// </summary>
    public class PersonSkill : BaseModel
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
        public string Details { get; set; }
    }
}