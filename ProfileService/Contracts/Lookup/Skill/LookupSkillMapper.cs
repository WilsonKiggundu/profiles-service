using AutoMapper;
using ProfileService.Contracts.Lookup.Skill;

namespace ProfileService.Contracts.Lookup.Skill
{
    public class LookupSkillMapper : Profile
    {
        public LookupSkillMapper()
        {                            
            CreateMap<Models.Common.Skill, NewLookupSkill>()
                .ReverseMap();

            CreateMap<Models.Common.Skill, UpdateLookupSkill>()
                .ReverseMap();

            CreateMap<Models.Common.Skill, GetLookupSkill>();
        }
    }
}