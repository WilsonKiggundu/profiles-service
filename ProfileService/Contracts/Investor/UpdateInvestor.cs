using System;
using ProfileService.Models.Investor;

namespace ProfileService.Contracts.Investor
{
    /// <summary>
    /// Update Investor
    /// </summary>
    public class UpdateInvestor
    {    
        public Guid Id { get; set; }
        public InvestorCategory InvestorCategory { get; set; }
        public Guid ProfileId { get; set; }
        public InvestorType InvestorType { get; set; }
        public string InvestmentTickets { get; set; }
        public InvestmentStage? InvestmentStage { get; set; }
    }
}