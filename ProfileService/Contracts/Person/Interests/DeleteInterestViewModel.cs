using System;

namespace ProfileService.Contracts.Person.Interests
{
    public class DeleteInterestViewModel
    {
        public Guid InterestId { get; set; }
        public Guid PersonId { get; set; }
    }
}