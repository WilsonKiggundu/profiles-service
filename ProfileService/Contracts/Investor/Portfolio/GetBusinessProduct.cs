using System;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Investor.Portfolio
{
    public class GetInvestorPortfolio : BaseModel
    {
        public Guid InvestorId { get; set; }
        public string Description { get; set; }
    }
}