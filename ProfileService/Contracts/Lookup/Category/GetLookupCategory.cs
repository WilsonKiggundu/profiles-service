using System;
using ProfileService.Contracts.Lookup.Upload;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Lookup.Category
{
    public class GetLookupCategory
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public GetLookupUpload Upload { get; set; }    
    }
}