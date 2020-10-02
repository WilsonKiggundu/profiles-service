using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Partner
{
    public class PartnerAddress : Address
    {
        public Guid PartnerId { get; set; }
        public Partner Partner { get; set; }
    }
}