using System;

namespace ProfileService.Contracts.Blog.Comment
{
    public class NewComment
    {
        public string Details { get; set; }
        public Guid AuthorId { get; set; }
        public Guid? PostId { get; set; }
        public Guid? ArticleId { get; set; }
    }
}