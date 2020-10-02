using System;
using System.ComponentModel.DataAnnotations;
using ProfileService.Models.Common;

namespace ProfileService.Models.Business
{
    public class BusinessProduct : BaseModel
    {
        [Required]
        public Guid BusinessId { get; set; }
        public Business Business { get; set; }

        [Required]
        public string Name { get; set; }
        
        [MaxLength(280)]
        public string Description { get; set; }

    }
}