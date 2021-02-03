using System;
using System.Collections.Generic;
using ProfileService.Contracts.Common;
using ProfileService.Models.Posts;

namespace ProfileService.Contracts.Blog.Post
{
    public class SearchPostRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Guid? AuthorId { get; set; }
        public Guid? PostId { get; set; }

        public Guid? UserId { get; set; }
    }
    
    public class SearchPostResponse
    {
        public SearchPostRequest Request { get; set; }
        public bool HasMore { get; set; }
        public bool AlreadyLikedByUser { get; set; } = false;
        public IEnumerable<Models.Posts.Post> Posts { get; set; }
    }
    
    public class SearchLikeRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Guid PostId { get; set; }
    }
    
    public class SearchLikeResponse
    {
        public SearchLikeRequest Request { get; set; }
        public bool HasMore { get; set; }
        public IEnumerable<Like> Likes { get; set; }
    }
    
    
}