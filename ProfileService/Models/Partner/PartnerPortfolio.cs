using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Partner
{
    public class PartnerPortfolio : BaseModel
    {
        public Guid PartnerId { get; set; }
        public Partner Partner { get; set; }

        public string Details { get; set; }
    }
}