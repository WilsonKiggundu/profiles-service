using System;
using ProfileService.Contracts.Lookup.Interest;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Investor.Interest
{
    /// <summary>
    /// Get a InvestorInterest
    /// </summary>
    public class GetInvestorInterest : BaseModel
    {
        public Guid InvestorId { get; set; }
        public GetLookupInterest Interest { get; set; }
    }
}