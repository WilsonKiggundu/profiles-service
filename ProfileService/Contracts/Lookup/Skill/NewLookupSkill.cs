using System;
using ProfileService.Contracts.Lookup.Upload;

namespace ProfileService.Contracts.Lookup.Skill
{
    public class NewLookupSkill
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }    
    }
}