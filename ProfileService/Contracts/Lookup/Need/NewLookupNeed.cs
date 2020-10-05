using System;
using ProfileService.Contracts.Lookup.Upload;

namespace ProfileService.Contracts.Lookup.Need
{
    public class NewLookupNeed
    {
        public string Category { get; set; }
        public GetLookupUpload Icon { get; set; }
    }
}