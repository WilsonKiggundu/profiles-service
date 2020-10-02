using AutoMapper;
using ProfileService.Contracts.Person.Skills;

namespace ProfileService.Contracts.Investor.Portfolio
{
    public class PersonSkillMapper : Profile
    {
        public PersonSkillMapper()
        {
            CreateMap<Models.Person.PersonSkill, GetPersonSkill>();
            CreateMap<NewPersonSkill, Models.Person.PersonSkill>()
                .ReverseMap();
            CreateMap<UpdatePersonSkill, Models.Person.PersonSkill>()
                .ReverseMap();
        }
    }
}