using System;
using ProfileService.Contracts.Lookup.Upload;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Lookup.Interest
{
    public class GetLookupInterest
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public GetLookupUpload Icon { get; set; }
    }
}