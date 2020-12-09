using System;
using System.Collections.Generic;
using ProfileService.Contracts.Blog.Comment;
using ProfileService.Contracts.Lookup.Upload;
using ProfileService.Contracts.Person;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Blog.Post
{
    public class GetPost : BaseModel
    {
        public string Details { get; set; }
        public PostType Type { get; set; }
        public Guid AuthorId { get; set; }
        public GetPerson Author { get; set; }
        public ICollection<GetLookupUpload> Uploads { get; set; }
        public ICollection<GetComment> Comments { get; set; }
    }

    public enum PostType
    {
        Post = 1,
        Photo = 2,
        Video = 3,
        Article = 4,
        Event = 5,
        Job = 6
    }
}