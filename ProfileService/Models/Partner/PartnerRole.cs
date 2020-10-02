using System;
using ProfileService.Models.Business;
using ProfileService.Models.Common;

namespace ProfileService.Models.Partner
{
    public class PartnerRole : BaseModel
    {
        public Guid PartnerId { get; set; }
        public Partner Partner { get; set; }

        public Guid PersonId { get; set; }
        public Person.Person Person { get; set; }

        public RoleType Type { get; set; }    
    }
}