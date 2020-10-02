using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Person
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonCategory : BaseModel
    {
        public Guid PersonId { get; set; }
        public Person Person { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}