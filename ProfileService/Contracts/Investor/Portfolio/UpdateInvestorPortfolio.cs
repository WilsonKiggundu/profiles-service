using System;

namespace ProfileService.Contracts.Investor.Portfolio
{
    public class UpdateInvestorPortfolio
    {
        public Guid Id { get; set; }    
        public Guid InvestorId { get; set; }    
        public string Description { get; set; }
    }
}