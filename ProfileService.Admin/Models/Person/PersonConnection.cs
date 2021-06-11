using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ProfileService.Models.Common;

namespace ProfileService.Models.Person
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