using System;
using System.Collections.Generic;
using ProfileService.Contracts.Common;

namespace ProfileService.Contracts.Blog.Comment
{
    public class SearchCommentsRequest
    {
        public Guid? PostId { get; set; }
        public Guid? ArticleId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class SearchCommentsResponse
    {
        public int Page { get; set; }
        public int PageSize { get; set; } 
        public bool HasMore { get; set; }
        public Guid? PostId { get; set; }
        public Guid? ArticleId { get; set; }
        public IEnumerable<Models.Posts.Comment> Comments { get; set; }
    }
}