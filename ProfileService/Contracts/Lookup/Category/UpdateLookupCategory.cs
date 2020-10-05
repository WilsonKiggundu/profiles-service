using System;
using ProfileService.Contracts.Lookup.Upload;

namespace ProfileService.Contracts.Lookup.Category
{
    public class UpdateLookupCategory
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public GetLookupUpload Icon { get; set; }
    }
}