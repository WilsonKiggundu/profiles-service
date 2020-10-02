using AutoMapper;

namespace ProfileService.Contracts.Business.Need
{
    public class BusinessNeedMapper : Profile
    {
        public BusinessNeedMapper()
        {
            CreateMap<Models.Business.BusinessNeed, GetBusinessNeed>();
            CreateMap<NewBusinessNeed, Models.Business.BusinessNeed>()
                .ReverseMap();
            CreateMap<UpdateBusinessNeed, Models.Business.BusinessNeed>()
                .ReverseMap();
        }
    }
}