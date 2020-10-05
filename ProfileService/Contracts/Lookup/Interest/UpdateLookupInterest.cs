using System;
using ProfileService.Contracts.Lookup.Upload;

namespace ProfileService.Contracts.Lookup.Interest
{
    public class UpdateLookupInterest
    {
        public Guid Id { get; set; }
        public string Category { get; set; }    
        public GetLookupUpload Icon { get; set; }
    }
}