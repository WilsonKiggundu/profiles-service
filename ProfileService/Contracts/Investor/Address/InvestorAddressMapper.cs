using AutoMapper;

namespace ProfileService.Contracts.Investor.Address
{
    public class InvestorAddressMapper : Profile
    {
        public InvestorAddressMapper()
        {
            CreateMap<Models.Investor.InvestorAddress, GetInvestorAddress>();
            CreateMap<NewInvestorAddress, Models.Investor.InvestorAddress>()
                .ReverseMap();
            CreateMap<UpdateInvestorAddress, Models.Investor.InvestorAddress>()
                .ReverseMap();
        }
    }
}