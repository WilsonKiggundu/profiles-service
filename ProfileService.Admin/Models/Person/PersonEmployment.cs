using System;

namespace ProfileService.Admin.Models.Person
{
    public class PersonEmployment : BaseModel
    {    
        public Guid PersonId { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string From { get; set; }
        public string Until { get; set; }
        public string Description { get; set; }
    }
}