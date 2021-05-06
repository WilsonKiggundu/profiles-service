using System;
using System.Collections.Generic;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Models;

namespace ProfileService.Contracts.FreelanceProject
{
    public class SearchFreelanceProjectRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Guid? OwnerId { get; set; }    
        public Guid? HiredPersonId { get; set; }

        public string Name { get; set; }    

        public ProjectStatus? Status { get; set; }
    }
    
    public class SearchFreelanceProjectResponse
    {
        public SearchFreelanceProjectRequest Request { get; set; }
        public bool HasMore { get; set; }
        public IEnumerable<Models.FreelanceProject> Projects { get; set; }
    }
    
}