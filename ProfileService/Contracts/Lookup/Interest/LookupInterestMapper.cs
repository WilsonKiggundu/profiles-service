using AutoMapper;

namespace ProfileService.Contracts.Lookup.Interest
{
    public class LookupInterestMapper : Profile
    {
        public LookupInterestMapper()
        {
            CreateMap<Models.Common.Interest, NewLookupInterest>()
                .ReverseMap();

            CreateMap<Models.Common.Interest, UpdateLookupInterest>()
                .ReverseMap();

            CreateMap<Models.Common.Interest, GetLookupInterest>();
        }
    }
}