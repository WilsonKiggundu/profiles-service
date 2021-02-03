using System;
using System.Collections.Generic;

namespace ProfileService.Contracts.Person.Skills
{
    public class NewPersonSkill
    {    
        public Guid PersonId { get; set; }
        public List<SkillViewModel> Skills { get; set; }
    }
    
    public class SkillViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}