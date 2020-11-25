using System;

namespace ProfileService.Contracts.Person.Skills
{
    public class NewPersonSkill
    {    
        public Guid PersonId { get; set; }
        public string Details { get; set; }    
    }
}