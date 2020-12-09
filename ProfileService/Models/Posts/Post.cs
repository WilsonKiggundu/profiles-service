using System;
using System.Collections.Generic;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Models.Common;

namespace ProfileService.Models.Posts
{
    public class Post : BaseModel
    {
        public string Details { get; set; }
        public PostType Type { get; set; } = PostType.Post;
        public ICollection<Upload> Uploads { get; set; }
        public ICollection<Comment> Comments { get; set; }
        
        public Guid AuthorId { get; set; }
        public Person.Person Author { get; set; }
    }
}