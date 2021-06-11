using System;

namespace ProfileService.Admin.Models.Person
{
    public class PersonProject : BaseModel
    {
        public Guid PersonId { get; set; }
        public string Title { get; set; }
        public string Client { get; set; }
        public string From { get; set; }
        public string Until { get; set; }
        public string Description { get; set; }   
        public string TechStack { get; set; }
        public string Role { get; set; }
        public string Url { get; set; }
        public string LinkToGitRepo { get; set; }
    }
}