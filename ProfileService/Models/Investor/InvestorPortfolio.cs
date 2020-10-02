using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Investor
{
    public class InvestorPortfolio : BaseModel
    {
        public Guid InvestorId { get; set; }
        public Investor Investor { get; set; }

        public string Details { get; set; }
    }
}