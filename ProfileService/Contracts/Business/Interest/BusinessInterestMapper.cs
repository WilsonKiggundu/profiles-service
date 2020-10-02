using AutoMapper;

namespace ProfileService.Contracts.Business.Interest
{
    public class BusinessInterestMapper : Profile
    {
        public BusinessInterestMapper()
        {
            CreateMap<Models.Business.BusinessInterest, GetBusinessInterest>();
            CreateMap<NewBusinessInterest, Models.Business.BusinessInterest>()
                .ReverseMap();
            CreateMap<UpdateBusinessInterest, Models.Business.BusinessInterest>()
                .ReverseMap();
        }
    }
}