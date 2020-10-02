using AutoMapper;

namespace ProfileService.Contracts.Business.Contact
{
    public class BusinessContactMapper : Profile
    {
        public BusinessContactMapper()
        {
            CreateMap<Models.Business.BusinessContact, GetBusinessContact>();
            CreateMap<NewBusinessContact, Models.Business.BusinessContact>()
                .ReverseMap();
            CreateMap<UpdateBusinessContact, Models.Business.BusinessContact>()
                .ReverseMap();
        }
    }
}