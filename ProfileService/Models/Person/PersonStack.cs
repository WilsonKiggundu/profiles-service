using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProfileService.Models.Common;

namespace ProfileService.Models.Person
{
    public class PersonStack : BaseModel
    {
        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public Guid StackId { get; set; }
        public TechStack Stack { get; set; }

        public string Level { get; set; }
    }
}