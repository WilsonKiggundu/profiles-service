using System;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Person.Skills
{
    public class GetPersonSkill : BaseModel
    {
        public Guid PersonId { get; set; }
        public string Details { get; set; } 
    }
}