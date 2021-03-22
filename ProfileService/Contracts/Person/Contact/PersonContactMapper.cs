using AutoMapper;

namespace ProfileService.Contracts.Person.Contact
{
    public class PersonContactMapper : Profile
    {
        public PersonContactMapper()
        {
            CreateMap<Models.Common.Contact, GetPersonContact>();
            CreateMap<NewPersonContact, Models.Common.Contact>()
                .ReverseMap();
            CreateMap<UpdatePersonContact, Models.Common.Contact>()
                .ReverseMap();
        }
    }
}