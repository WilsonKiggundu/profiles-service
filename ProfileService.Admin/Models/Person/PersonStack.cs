using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Admin.Models.Person
{
    public class PersonStack : BaseModel
    {
        [Required] public Guid PersonId { get; set; }
        [Required] public string Stack { get; set; }
        
        [Required] public string Level { get; set; }
    }
}