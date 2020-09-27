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
            CreateMap<Models.Person, GetPerson>();
            CreateMap<NewPerson, Models.Person>()
                .ReverseMap();
            CreateMap<UpdatePerson, Models.Person>()
                .ReverseMap();
        }
    }
}