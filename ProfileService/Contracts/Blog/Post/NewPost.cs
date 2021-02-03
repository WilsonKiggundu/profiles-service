using System;
using System.Collections.Generic;
using ProfileService.Contracts.Lookup.Upload;
using ProfileService.Contracts.Person;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Blog.Post
{
    public class NewPost : BaseModel
    {
        public string Details { get; set; }
        public Guid? ReferenceId { get; set; }
        public Guid AuthorId { get; set; }
        public GetPerson Author { get; set; }
        public string Uploads { get; set; }
    }
}