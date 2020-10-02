using AutoMapper;

namespace ProfileService.Contracts.Person.Awards
{
    public class PersonAwardMapper : Profile
    {
        public PersonAwardMapper()
        {
            CreateMap<Models.Person.PersonAward, GetPersonAward>();
            CreateMap<NewPersonAward, Models.Person.PersonAward>()
                .ReverseMap();
            CreateMap<UpdatePersonAward, Models.Person.PersonAward>()
                .ReverseMap();
        }
    }
}