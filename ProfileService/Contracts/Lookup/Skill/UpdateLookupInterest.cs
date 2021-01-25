using System;
using ProfileService.Contracts.Lookup.Upload;

namespace ProfileService.Contracts.Lookup.Skill
{
    public class UpdateLookupSkill
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
    }
}