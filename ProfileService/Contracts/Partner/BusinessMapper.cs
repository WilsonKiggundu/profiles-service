using AutoMapper;

namespace ProfileService.Contracts.Partner
{
    public class PartnerMapper : Profile
    {
        public PartnerMapper()
        {
            CreateMap<Models.Partner.Partner, GetPartner>();
            CreateMap<NewPartner, Models.Partner.Partner>()
                .ReverseMap();
            CreateMap<UpdatePartner, Models.Partner.Partner>()
                .ReverseMap();
        }
    }
}