using System;
using System.Collections.Generic;
using ProfileService.Contracts.Lookup.Upload;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Person.Awards
{
    public class GetPersonAward : BaseModel
    {
        public Guid PersonId { get; set; }
        public string AwardedBy { get; set; }
        public string Title { get; set; }
        public string FieldOfStudy { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
        public string Grade { get; set; }
        public string Activities { get; set; }
        public string Description { get; set; }
    }
}