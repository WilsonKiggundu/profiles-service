using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProfileService.Models.Common;

namespace ProfileService.Models.Person
{
    public class PersonStack : BaseModel
    {
        [Required] public Guid PersonId { get; set; }
        [Required] public string Stack { get; set; }
        
        [Required] public string Level { get; set; }
    }
}