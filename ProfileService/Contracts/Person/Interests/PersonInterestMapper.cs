using AutoMapper;

namespace ProfileService.Contracts.Person.Interests
{
    public class PersonInterestMapper : Profile
    {
        public PersonInterestMapper()
        {
            CreateMap<Models.Person.PersonInterest, GetPersonInterest>();
            CreateMap<NewPersonInterest, Models.Person.PersonInterest>()
                .ReverseMap();
            CreateMap<UpdatePersonInterest, Models.Person.PersonInterest>()
                .ReverseMap();
        }
    }
}