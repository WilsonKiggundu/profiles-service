using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProfileService.Models.Common;

namespace ProfileService.Models.Business
{
    public class BusinessProduct : BaseModel
    {
        [Required]
        public Guid BusinessId { get; set; }
        
        [JsonIgnore]
        public Business Business { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }

    }
}