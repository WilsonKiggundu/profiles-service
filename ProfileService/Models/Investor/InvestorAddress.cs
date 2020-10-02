using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Investor
{
    public class InvestorAddress : Address
    {
        public Guid InvestorId { get; set; }
        public Investor Investor { get; set; }
    }
}