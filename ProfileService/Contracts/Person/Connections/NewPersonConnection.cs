using System;

namespace ProfileService.Contracts.Person.Connections
{
    public class NewPersonConnection
    {    
        public Guid? Id { get; set; }
        public Guid FollowerId { get; set; }
        public Guid PersonId { get; set; }
    }
}