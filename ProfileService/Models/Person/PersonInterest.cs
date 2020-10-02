using System;
using System.ComponentModel.DataAnnotations;
using ProfileService.Models.Common;

namespace ProfileService.Models.Person
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonInterest : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Guid PersonId { get; set; }
        public Person Person { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Guid InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}