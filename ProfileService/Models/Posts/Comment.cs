using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Posts
{
    public class Comment : BaseModel
    {
        public string Details { get; set; }
        public Guid? PostId { get; set; }
        public Post Post { get; set; }

        public Guid? ArticleId { get; set; }
        public Article Article { get; set; }

        public Guid AuthorId { get; set; }
        public Person.Person Author { get; set; }
    }
}