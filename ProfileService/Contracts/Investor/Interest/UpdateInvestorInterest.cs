using System;

namespace ProfileService.Contracts.Investor.Interest
{
    public class UpdateInvestorInterest
    {
        public Guid Id { get; set; }    
        public Guid InvestorId { get; set; }
        public Guid InterestId { get; set; } 
    }
}