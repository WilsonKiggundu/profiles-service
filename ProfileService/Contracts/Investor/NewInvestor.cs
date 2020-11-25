using System;
using ProfileService.Models.Investor;

namespace ProfileService.Contracts.Investor
{
    /// <summary>
    /// New Investor
    /// </summary>
    public class NewInvestor
    {
        public InvestorCategory InvestorCategory { get; set; }
        public Guid ProfileId { get; set; }
        public InvestorType InvestorType { get; set; }
        public string InvestmentTickets { get; set; }
        public InvestmentStage? InvestmentStage { get; set; }
        
    }

    public enum InvestmentStage
    {
        PreSeed = 1,
        Seed = 2,
        SeriesA = 3,
        SeriesB = 4,
        SeriesC = 5,
        Other = 6
    }

    public enum InvestorCategory
    {
        Person = 1,
        Business = 2
    }
}