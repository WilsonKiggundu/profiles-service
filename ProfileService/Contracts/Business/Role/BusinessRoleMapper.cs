using AutoMapper;

namespace ProfileService.Contracts.Business.Role
{
    public class BusinessRoleMapper : Profile
    {
        public BusinessRoleMapper()
        {
            CreateMap<Models.Business.BusinessRole, GetBusinessRole>();
            CreateMap<NewBusinessRole, Models.Business.BusinessRole>()
                .ReverseMap();
            CreateMap<UpdateBusinessRole, Models.Business.BusinessRole>()
                .ReverseMap();
        }
    }
}