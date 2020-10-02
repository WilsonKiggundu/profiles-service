using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Partner
{
    public class PartnerContribution
    {
        public Guid PartnerId { get; set; }
        public Partner Partner { get; set; }

        public Guid ContributionId { get; set; }
        public Contribution Contribution { get; set; }
    }
}