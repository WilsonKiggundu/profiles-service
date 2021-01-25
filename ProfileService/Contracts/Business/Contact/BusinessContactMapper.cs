using AutoMapper;

namespace ProfileService.Contracts.Business.Contact
{
    public class BusinessContactMapper : Profile
    {
        public BusinessContactMapper()
        {
            CreateMap<Models.Common.Contact, GetBusinessContact>();
            CreateMap<NewBusinessContact, Models.Common.Contact>()
                .ReverseMap();
            CreateMap<UpdateBusinessContact, Models.Common.Contact>()
                .ReverseMap();
        }
    }
}