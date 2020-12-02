using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Business
{
    public class BusinessRole : BaseModel
    {
        public Guid BusinessId { get; set; }
        public Business Business { get; set; }

        public Guid PersonId { get; set; }
        public Person.Person Person { get; set; }

        public RoleType Role { get; set; }
        
    }

    public enum RoleType
    {
        Founder = 1,
        Cofounder = 2,
        Director = 3,
        Shareholder = 4,
        Manager = 5,
        Ceo = 6,
        Coo = 7,
        Pr = 8,
        Marketing = 9,
        Hr = 10,
        PageAdmin = 0
    }
}