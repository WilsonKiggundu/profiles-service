using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Models.Common;

namespace ProfileService.Models.Posts
{
    public class Post : BaseModel
    {
        public string Details { get; set; }
        public PostType Type { get; set; } = PostType.Post;
        public Guid? ReferenceId { get; set; }
        
        public ICollection<Upload> Uploads { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }

        public Guid AuthorId { get; set; }
        public Person.Person Author { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }

        [NotMapped]
        public int CommentsCount { get; set; }
        
        [NotMapped]
        public int LikesCount { get; set; }
        
        [NotMapped] public bool AlreadyLikedByUser { get; set; }
    }
}