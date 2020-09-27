using AutoMapper;

namespace ProfileService.Contracts.Contact
{
    /// <summary>
    /// 
    /// </summary>
    public class ContactMapper : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public ContactMapper()
        {
            CreateMap<Models.Contact, GetContact>();
            CreateMap<NewContact, Models.Contact>()
                .ReverseMap();
            CreateMap<UpdateContact, Models.Contact>()
                .ReverseMap();
        }
    }
}