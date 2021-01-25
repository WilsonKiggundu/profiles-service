using AutoMapper;
using ProfileService.Contracts.Person.Skills;

namespace ProfileService.Contracts.Person.Connections
{
    public class PersonConnectionMapper : Profile
    {
        public PersonConnectionMapper()
        {
            CreateMap<Models.Person.PersonConnection, GetPersonConnection>();
            CreateMap<NewPersonConnection, Models.Person.PersonConnection>()
                .ReverseMap();
            CreateMap<UpdatePersonConnection, Models.Person.PersonConnection>()
                .ReverseMap();
        }
    }
}