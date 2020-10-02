using AutoMapper;

namespace ProfileService.Contracts.Person
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonMapper : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public PersonMapper()
        {
            CreateMap<Models.Person.Person, GetPerson>();
            CreateMap<NewPerson, Models.Person.Person>()
                .ReverseMap();
            CreateMap<UpdatePerson, Models.Person.Person>()
                .ReverseMap();
        }
    }
}