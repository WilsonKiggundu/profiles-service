using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Partner
{
    public class PartnerInterest : BaseModel
    {
        public Guid PartnerId { get; set; }
        public Partner Partner { get; set; }

        public Guid InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}