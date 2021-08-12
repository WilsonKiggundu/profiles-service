using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ProfileService.Models.Common;

namespace ProfileService.Models.Posts
{
    public class Article : BaseModel
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public string Summary { get; set; }   
        public Guid AuthorId { get; set; }
        
        // [JsonIgnore]
        public Person.Person Author { get; set; }

        public PublishStatus Status { get; set; }   

        public ICollection<ArticleCategory> Categories { get; set; }
        public ICollection<ArticleTag> Tags { get; set; }   
        public ICollection<Upload> Uploads { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
    }

    public enum PublishStatus
    {
        Draft = 1,
        Published = 2
    }
}