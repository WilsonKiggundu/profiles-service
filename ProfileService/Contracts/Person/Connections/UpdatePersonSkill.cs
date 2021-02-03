using System;

namespace ProfileService.Contracts.Person.Connections
{
    public class UpdatePersonConnection
    {
        public Guid Id { get; set; }    
        public Guid FollowerId { get; set; }
        public Guid FolloweeId { get; set; }
    }
}