using System;
using ProfileService.Contracts.Person;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Blog.Comment
{
    public class NewComment : BaseModel
    {
        public string Details { get; set; }
        public Guid AuthorId { get; set; }
        public GetPerson Author { get; set; }
        public Guid? PostId { get; set; }
        public Guid? ArticleId { get; set; }
    }
}