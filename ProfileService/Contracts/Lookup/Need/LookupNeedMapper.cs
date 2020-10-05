using AutoMapper;

namespace ProfileService.Contracts.Lookup.Need
{
    public class LookupNeedMapper : Profile
    {
        public LookupNeedMapper()
        {
            CreateMap<Models.Common.Need, NewLookupNeed>().ReverseMap();
            CreateMap<Models.Common.Need, UpdateLookupNeed>().ReverseMap();
            CreateMap<Models.Common.Need, GetLookupNeed>();
        }
    }
}