using AutoMapper;

namespace ProfileService.Contracts.Partner.Contact
{
    public class PartnerContactMapper : Profile
    {
        public PartnerContactMapper()
        {
            CreateMap<Models.Partner.PartnerContact, GetPartnerContact>();
            CreateMap<NewPartnerContact, Models.Partner.PartnerContact>()
                .ReverseMap();
            CreateMap<UpdatePartnerContact, Models.Partner.PartnerContact>()
                .ReverseMap();
        }
    }
}