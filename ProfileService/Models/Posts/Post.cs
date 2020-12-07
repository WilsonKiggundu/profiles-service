using System;
using System.Collections.Generic;
using ProfileService.Models.Common;

namespace ProfileService.Models.Posts
{
    public class Post : BaseModel
    {
        public string Details { get; set; }
        public ICollection<Upload> Uploads { get; set; }
        
        public Guid AuthorId { get; set; }
        public Person.Person Author { get; set; }
    }
}