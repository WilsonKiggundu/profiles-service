using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Investor
{
    public class InvestorInterest : BaseModel
    {
        public Guid InvestorId { get; set; }
        public Investor Investor { get; set; }

        public Guid InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}