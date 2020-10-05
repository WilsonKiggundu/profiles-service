using System;
using ProfileService.Contracts.Lookup.Upload;

namespace ProfileService.Contracts.Lookup.Need
{
    public class UpdateLookupNeed
    {    
        public Guid Id { get; set; }
        public string Category { get; set; }
        public GetLookupUpload Icon { get; set; }
    }
}