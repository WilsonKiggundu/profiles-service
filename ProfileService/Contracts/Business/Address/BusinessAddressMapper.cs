using AutoMapper;

namespace ProfileService.Contracts.Business.Address
{
    public class BusinessAddressMapper : Profile
    {
        public BusinessAddressMapper()
        {
            CreateMap<Models.Business.BusinessAddress, GetBusinessAddress>();
            CreateMap<NewBusinessAddress, Models.Business.BusinessAddress>()
                .ReverseMap();
            CreateMap<UpdateBusinessAddress, Models.Business.BusinessAddress>()
                .ReverseMap();
        }
    }
}