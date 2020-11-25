using System;

namespace ProfileService.Contracts.Investor.Interest
{
    /// <summary>
    /// New InvestorInterest
    /// </summary>
    public class NewInvestorInterest
    {
        public Guid InvestorId { get; set; }
        public Guid InterestId { get; set; }    
    }
}