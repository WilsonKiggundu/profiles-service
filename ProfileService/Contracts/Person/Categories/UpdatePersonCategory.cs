using System;

namespace ProfileService.Contracts.Person.Categories
{
    public class UpdatePersonCategory
    {    
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PersonId { get; set; } 
    }
}