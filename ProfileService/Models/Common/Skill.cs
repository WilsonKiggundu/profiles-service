using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models.Common
{
    public class Skill : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}