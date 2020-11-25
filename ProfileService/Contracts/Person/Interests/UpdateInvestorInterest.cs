using System;

namespace ProfileService.Contracts.Person.Interests
{
    public class UpdatePersonInterest
    {
        public Guid Id { get; set; }    
        public Guid PersonId { get; set; }    
        public Guid InterestId { get; set; }
    }
}