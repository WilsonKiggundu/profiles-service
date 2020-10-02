using AutoMapper;

namespace ProfileService.Contracts.Investor.Contact
{
    public class InvestorContactMapper : Profile
    {
        public InvestorContactMapper()
        {
            CreateMap<Models.Investor.InvestorContact, GetInvestorContact>();
            CreateMap<NewInvestorContact, Models.Investor.InvestorContact>()
                .ReverseMap();
            CreateMap<UpdateInvestorContact, Models.Investor.InvestorContact>()
                .ReverseMap();
        }
    }
}