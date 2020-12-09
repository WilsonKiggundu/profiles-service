using System;
using System.Collections.Generic;
using ProfileService.Contracts.Lookup.Upload;

namespace ProfileService.Contracts.Blog.Post
{
    public class NewPost
    {
        public string Details { get; set; }
        public Guid AuthorId { get; set; }
        
        public string Uploads { get; set; }
    }
}