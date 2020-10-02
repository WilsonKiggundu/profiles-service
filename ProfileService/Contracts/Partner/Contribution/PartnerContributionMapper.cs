using AutoMapper;

namespace ProfileService.Contracts.Partner.Contribution
{
    public class PartnerContributionMapper : Profile
    {
        public PartnerContributionMapper()
        {
            CreateMap<Models.Partner.PartnerContribution, GetPartnerContribution>();
            CreateMap<NewPartnerContribution, Models.Partner.PartnerContribution>()
                .ReverseMap();
            CreateMap<UpdatePartnerContribution, Models.Partner.PartnerContribution>()
                .ReverseMap();
        }
    }
}