using System;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Person.Connections
{
    public class GetPersonConnection : BaseModel
    {
        public Guid FollowerId { get; set; }
        public Guid PersonId { get; set; }    
        public GetPerson Person { get; set; }
    }
}