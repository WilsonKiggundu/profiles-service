using AutoMapper;

namespace ProfileService.Contracts.Lookup.Category
{
    public class LookupCategoryMapper : Profile
    {
        public LookupCategoryMapper()
        {
            CreateMap<Models.Common.Category, NewLookupCategory>()
                .ReverseMap();

            CreateMap<Models.Common.Category, UpdateLookupCategory>()
                .ReverseMap();

            CreateMap<Models.Common.Category, GetLookupCategory>();

        }
    }
}