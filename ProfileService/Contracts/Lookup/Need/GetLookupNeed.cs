using System;
using ProfileService.Contracts.Lookup.Upload;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Lookup.Need
{
    public class GetLookupNeed
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public GetLookupUpload Icon { get; set; }
    }
}