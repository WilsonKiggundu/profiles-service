using System;
using System.Collections.Generic;
using ProfileService.Contracts.Lookup.Upload;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Person.Awards
{
    public class NewPersonAward : BaseModel
    {
        public Guid PersonId { get; set; }
        public string Title { get; set; }
        public string FieldOfStudy { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
        public string Grade { get; set; }
        public string Activities { get; set; }
        public string Description { get; set; }
        public SchoolViewModel Institute { get; set; } 
    }

    public class SchoolViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}