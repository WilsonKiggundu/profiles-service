using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Person
{
    public class PersonProject : BaseModel
    {
        public Guid PersonId { get; set; }
        public string Title { get; set; }
        public string StartYear { get; set; }
        public string StartMonth { get; set; }
        public string EndYear { get; set; }
        public string EndMonth { get; set; }
        public string Description { get; set; }   
        public string TechStack { get; set; }
        public string Role { get; set; }
        public string Url { get; set; }
        public string LinkToGitRepo { get; set; }
    }
}