using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Person
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonAward : BaseModel
    {
        public Guid PersonId { get; set; }
        public Guid? InstituteId { get; set; }
        public School Institute { get; set; }
        
        public string Title { get; set; }
        public string FieldOfStudy { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
        public string Grade { get; set; }
        public string Activities { get; set; }
        public string Description { get; set; }
        
    }
}