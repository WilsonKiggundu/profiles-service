using AutoMapper;

namespace ProfileService.Contracts.Person.Categories
{
    public class PersonCategoryMapper : Profile
    {
        public PersonCategoryMapper()
        {
            CreateMap<Models.Person.PersonCategory, GetPersonCategory>();
            CreateMap<NewPersonCategory, Models.Person.PersonCategory>()
                .ReverseMap();
            CreateMap<UpdatePersonCategory, Models.Person.PersonCategory>()
                .ReverseMap();
        }
    }
}