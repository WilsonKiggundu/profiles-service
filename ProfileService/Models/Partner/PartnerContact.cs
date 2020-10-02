using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Partner
{
    public class PartnerContact : BaseModel
    {
        public Guid PartnerId { get; set; }
        public Partner Partner { get; set; }

        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}