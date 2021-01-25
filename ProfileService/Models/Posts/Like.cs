using System;
using System.Text.Json.Serialization;
using ProfileService.Models.Common;

namespace ProfileService.Models.Posts
{
    public class Like : BaseModel
    {
        public Guid EntityId { get; set; }
        public Guid PersonId { get; set; }
        public Person.Person Person { get; set; }
    }
}