using System;
using System.Text.Json.Serialization;

namespace ProfileService.Admin.Models.Person
{
    /// <summary>
    /// Person Connection
    /// </summary>
    public class PersonConnection : BaseModel
    {
        public Guid FollowerId { get; set; }
        public Guid PersonId { get; set; }
        
        [JsonIgnore]
        public Person Person { get; set; }
    }
}