using AutoMapper;

namespace ProfileService.Contracts.Investor
{
    public class InvestorMapper : Profile
    {
        public InvestorMapper()
        {
            CreateMap<Models.Investor.Investor, GetInvestor>();
            CreateMap<NewInvestor, Models.Investor.Investor>()
                .ReverseMap();
            CreateMap<UpdateInvestor, Models.Investor.Investor>()
                .ReverseMap();
        }
    }
}