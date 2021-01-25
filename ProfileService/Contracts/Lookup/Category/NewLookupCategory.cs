using System;
using ProfileService.Contracts.Lookup.Upload;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Lookup.Category
{
    public class NewLookupCategory : BaseModel
    {
        public Guid? PersonId { get; set; }
        public string Name { get; set; }
        public GetLookupUpload Icon { get; set; }
    }
}