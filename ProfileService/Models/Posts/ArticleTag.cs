using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Posts
{
    public class ArticleTag : BaseModel
    {
        public string Label { get; set; }
        public Guid CreatedBy { get; set; }
    }
}