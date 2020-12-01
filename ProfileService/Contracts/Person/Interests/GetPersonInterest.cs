using System;
using ProfileService.Contracts.Lookup.Interest;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Person.Interests
{
    public class GetPersonInterest : BaseModel
    {
        public Guid PersonId { get; set; }
        
        public GetLookupInterest Interest { get; set; }
        public Guid InterestId { get; set; }
    }
}