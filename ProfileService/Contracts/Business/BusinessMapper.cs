using AutoMapper;

namespace ProfileService.Contracts.Business
{
    public class BusinessMapper : Profile
    {
        public BusinessMapper()
        {
            CreateMap<Models.Business.Business, GetBusiness>();
            CreateMap<NewBusiness, Models.Business.Business>()
                .ReverseMap();
            CreateMap<UpdateBusiness, Models.Business.Business>()
                .ReverseMap();
        }
    }
}