using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Person
{
    public class PersonEmployment : BaseModel
    {    
        public Guid PersonId { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string StartYear { get; set; }
        public string StartMonth { get; set; }
        public string EndYear { get; set; }    
        public string EndMonth { get; set; }
        public string Description { get; set; }
    }
}