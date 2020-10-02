using AutoMapper;

namespace ProfileService.Contracts.Investor.Interest
{
    public class InvestorInterestMapper : Profile
    {
        public InvestorInterestMapper()
        {
            CreateMap<Models.Investor.InvestorInterest, GetInvestorInterest>();
            CreateMap<NewInvestorInterest, Models.Investor.InvestorInterest>()
                .ReverseMap();
            CreateMap<UpdateInvestorInterest, Models.Investor.InvestorInterest>()
                .ReverseMap();
        }
    }
}