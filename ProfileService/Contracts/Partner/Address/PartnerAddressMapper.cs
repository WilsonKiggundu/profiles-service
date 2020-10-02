using AutoMapper;

namespace ProfileService.Contracts.Partner.Address
{
    public class PartnerAddressMapper : Profile
    {
        public PartnerAddressMapper()
        {
            CreateMap<Models.Partner.PartnerAddress, GetPartnerAddress>();
            CreateMap<NewPartnerAddress, Models.Partner.PartnerAddress>()
                .ReverseMap();
            CreateMap<UpdatePartnerAddress, Models.Partner.PartnerAddress>()
                .ReverseMap();
        }
    }
}