using System;
using System.Collections.Generic;
using ProfileService.Models.Common;
using ProfileService.Models.Posts;

namespace ProfileService.Contracts.Blog.Article
{
    public class NewArticle
    {
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Details { get; set; }
        public List<Upload> Uploads { get; set; }
        public PublishStatus Status { get; set; } = PublishStatus.Published;
    }
}