using System;
using System.Text.Json.Serialization;
using ProfileService.Models.Common;

namespace ProfileService.Models.Business
{
    public class BusinessAddress : Address
    {
        public Guid BusinessId { get; set; }  
        
        [JsonIgnore]
        public Business Business { get; set; }
    }

}