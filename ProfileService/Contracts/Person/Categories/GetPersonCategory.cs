using System;
using ProfileService.Models.Common;
using ProfileService.Models.Person;

namespace ProfileService.Contracts.Person.Categories
{
    public class GetPersonCategory : BaseModel
    {
        public Guid CategoryId { get; set; }        
        public PersonCategory Category { get; set; }
        
        public Guid PersonId { get; set; } 
    }
}