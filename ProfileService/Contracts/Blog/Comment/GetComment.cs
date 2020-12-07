using System;
using ProfileService.Contracts.Person;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Blog.Comment
{
    public class GetComment : BaseModel
    {
        public Guid? PostId { get; set; }
        public Guid? ArticleId { get; set; }
        public string Details { get; set; }
        public GetPerson Author { get; set; }
    }
}