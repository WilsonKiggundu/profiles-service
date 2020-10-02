using AutoMapper;

namespace ProfileService.Contracts.Investor.Portfolio
{
    public class InvestorPortfolioMapper : Profile
    {
        public InvestorPortfolioMapper()
        {
            CreateMap<Models.Investor.InvestorPortfolio, GetInvestorPortfolio>();
            CreateMap<NewInvestorPortfolio, Models.Investor.InvestorPortfolio>()
                .ReverseMap();
            CreateMap<UpdateInvestorPortfolio, Models.Investor.InvestorPortfolio>()
                .ReverseMap();
        }
    }
}