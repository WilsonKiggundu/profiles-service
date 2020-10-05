using AutoMapper;

namespace ProfileService.Contracts.Lookup.Upload
{
    public class LookupUploadMapper : Profile
    {
        public LookupUploadMapper()
        {
            CreateMap<Models.Common.Upload, NewLookupUpload>().ReverseMap();
            CreateMap<Models.Common.Upload, UpdateLookupUpload>().ReverseMap();
            CreateMap<Models.Common.Upload, GetLookupUpload>();
        }
    }
}