using System;
using ProfileService.Contracts.Lookup.Upload;

namespace ProfileService.Contracts.Lookup.Interest
{
    public class NewLookupInterest
    {
        public string Category { get; set; }
        public GetLookupUpload Icon { get; set; }
    }
}