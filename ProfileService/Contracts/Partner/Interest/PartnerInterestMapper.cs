using AutoMapper;

namespace ProfileService.Contracts.Partner.Interest
{
    public class PartnerInterestMapper : Profile
    {
        public PartnerInterestMapper()
        {
            CreateMap<Models.Partner.PartnerInterest, GetPartnerInterest>();
            CreateMap<NewPartnerInterest, Models.Partner.PartnerInterest>()
                .ReverseMap();
            CreateMap<UpdatePartnerInterest, Models.Partner.PartnerInterest>()
                .ReverseMap();
        }
    }
}