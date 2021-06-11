using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProfileService.Models.Common;

namespace ProfileService.Models.Person
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