using System;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Blog.Comment
{
    public class UpdateComment : BaseModel
    {
        public string Details { get; set; }
        public Guid? PostId { get; set; }
        public Guid? ArticleId { get; set; }
        public Guid AuthorId { get; set; }
    }
}