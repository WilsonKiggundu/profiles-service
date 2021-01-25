using System;
using System.Text.Json.Serialization;
using ProfileService.Models.Common;

namespace ProfileService.Models.Business
{
    public class BusinessInterest : BaseModel
    {
        public Guid BusinessId { get; set; }
        [JsonIgnore]
        public Business Business { get; set; }    

        public Guid InterestId { get; set; }
        public Interest Interest { get; set; }    
    }
}