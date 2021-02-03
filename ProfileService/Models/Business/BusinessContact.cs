using System;
using System.Text.Json.Serialization;
using ProfileService.Models.Common;

namespace ProfileService.Models.Business
{
    public class BusinessContact : BaseModel
    {
        public Guid BusinessId { get; set; }
        
        [JsonIgnore]
        public Business Business { get; set; }

        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}