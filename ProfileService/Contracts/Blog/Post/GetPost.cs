using System;
using ProfileService.Contracts.Person;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Blog.Post
{
    public class GetPost : BaseModel
    {
        public string Details { get; set; }
        public Guid AuthorId { get; set; }
        public GetPerson Author { get; set; }
    }
}