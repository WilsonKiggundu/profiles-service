using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Posts
{
    public class ArticleCategory : BaseModel
    {
        public string Label { get; set; }
        public Guid CreatedBy { get; set; }
    }
}