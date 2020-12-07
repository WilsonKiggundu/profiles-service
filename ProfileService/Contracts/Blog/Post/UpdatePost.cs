using System;
using System.Collections.Generic;
using ProfileService.Contracts.Lookup.Upload;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Blog.Post
{
    public class UpdatePost : BaseModel
    {
        public string Details { get; set; }
        public Guid AuthorId { get; set; }
        
        // public ICollection<GetLookupUpload> Uploads { get; set; }
    }
}