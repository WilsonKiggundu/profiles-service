using System;

namespace ProfileService.Contracts.Investor.Portfolio
{
    public class NewInvestorPortfolio
    {
        public Guid InvestorId { get; set; }    
        public string Description { get; set; }
    }
}