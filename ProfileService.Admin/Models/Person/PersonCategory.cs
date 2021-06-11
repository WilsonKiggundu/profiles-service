using System;
using ProfileService.Admin.Models.Common;

namespace ProfileService.Admin.Models.Person
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonCategory : BaseModel
    {
        public Guid PersonId { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}