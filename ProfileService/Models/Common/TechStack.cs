using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models.Common
{
    public class TechStack : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}