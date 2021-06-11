using System;
using System.ComponentModel.DataAnnotations;
using ProfileService.Admin.Models.Common;

namespace ProfileService.Admin.Models.Person
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonInterest : BaseModel
    {
        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public Guid InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}