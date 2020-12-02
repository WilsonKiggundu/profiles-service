using System;
using ProfileService.Contracts.Lookup.Interest;

namespace ProfileService.Contracts.Business.Interest
{
    /// <summary>
    /// Get a BusinessInterest
    /// </summary>
    public class GetBusinessInterest
    {
        public Guid BusinessId { get; set; }
        
        public GetLookupInterest Interest { get; set; }
        public Guid InterestId { get; set; }
    }
}