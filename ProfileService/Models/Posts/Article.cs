using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Posts
{
    public class Article : BaseModel
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public Guid AuthorId { get; set; }
        public Person.Person Author { get; set; }
        public PublishStatus Status { get; set; }
    }

    public enum PublishStatus
    {
        Draft = 1,
        Published = 2
    }
}