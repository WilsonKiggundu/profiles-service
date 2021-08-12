using System;
using System.Collections.Generic;

namespace ProfileService.Contracts.Blog.Article
{
    public class SearchArticleRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Guid? AuthorId { get; set; }
        public Guid? ArticleId { get; set; }

        public string Title { get; set; }
    }
    
    public class SearchArticleResponse
    {
        public SearchArticleRequest Request { get; set; }
        public bool HasMore { get; set; }
        public ICollection<Models.Posts.Article> Articles { get; set; }
    }
}