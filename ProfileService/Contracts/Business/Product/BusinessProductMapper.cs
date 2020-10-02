using AutoMapper;

namespace ProfileService.Contracts.Business.Product
{
    public class BusinessProductMapper : Profile
    {
        public BusinessProductMapper()
        {
            CreateMap<Models.Business.BusinessProduct, GetBusinessProduct>();
            CreateMap<NewBusinessProduct, Models.Business.BusinessProduct>()
                .ReverseMap();
            CreateMap<UpdateBusinessProduct, Models.Business.BusinessProduct>()
                .ReverseMap();
        }
    }
}