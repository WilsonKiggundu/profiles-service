using System;

namespace ProfileService.Contracts.Person.Categories
{
    public class NewPersonCategory
    {
        public Guid CategoryId { get; set; }
        public Guid PersonId { get; set; }    
    }
}