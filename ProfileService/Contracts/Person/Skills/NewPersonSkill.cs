using System;

namespace ProfileService.Contracts.Person.Skills
{
    public class NewPersonSkill
    {    
        public Guid? Id { get; set; }
        public Guid PersonId { get; set; }
        public string Details { get; set; }    
    }
}