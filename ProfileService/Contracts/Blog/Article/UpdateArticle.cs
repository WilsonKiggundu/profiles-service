using System;
using System.Collections.Generic;
using ProfileService.Contracts.Lookup.Upload;
using ProfileService.Models.Common;
using ProfileService.Models.Posts;

namespace ProfileService.Contracts.Blog.Article
{
    public class UpdateArticle
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Details { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Tags { get; set; }
        public List<GetLookupUpload> Uploads { get; set; }
        public PublishStatus Status { get; set; } = PublishStatus.Published;
    }
}