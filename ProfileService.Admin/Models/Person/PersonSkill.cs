using System;
using ProfileService.Admin.Models.Common;

namespace ProfileService.Admin.Models.Person
{
    /// <summary>
    /// Person skill
    /// </summary>
    public class PersonSkill : BaseModel
    {
        public Guid PersonId { get; set; }
        public Guid? SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}