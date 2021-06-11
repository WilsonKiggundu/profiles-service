using System;
using System.Text.Json.Serialization;
using ProfileService.Models.Common;

namespace ProfileService.Models.Person
{
    public class PersonContact : BaseModel
    {
        public Guid PersonId { get; set; }
        
        [JsonIgnore]
        public Person Person { get; set; }

        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}