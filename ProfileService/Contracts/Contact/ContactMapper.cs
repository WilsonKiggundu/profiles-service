using AutoMapper;
using ProfileService.Contracts.Business;

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
            CreateMap<Models.Common.Contact, GetContact>();
            CreateMap<NewContact, Models.Common.Contact>()
                .ReverseMap();
            CreateMap<UpdateContact, Models.Common.Contact>()
                .ReverseMap();
        }
    }
}