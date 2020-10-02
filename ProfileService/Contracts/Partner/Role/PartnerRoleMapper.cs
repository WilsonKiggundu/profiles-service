using AutoMapper;

namespace ProfileService.Contracts.Partner.Role
{
    public class PartnerRoleMapper : Profile
    {
        public PartnerRoleMapper()
        {
            CreateMap<Models.Partner.PartnerRole, GetPartnerRole>();
            CreateMap<NewPartnerRole, Models.Partner.PartnerRole>()
                .ReverseMap();
            CreateMap<UpdatePartnerRole, Models.Partner.PartnerRole>()
                .ReverseMap();
        }
    }
}