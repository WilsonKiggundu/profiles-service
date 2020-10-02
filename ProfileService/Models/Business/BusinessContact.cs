using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Business
{
    public class BusinessContact : BaseModel
    {
        public Guid BusinessId { get; set; }
        public Business Business { get; set; }

        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}