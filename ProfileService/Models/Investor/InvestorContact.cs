using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Investor
{
    public class InvestorContact : BaseModel
    {
        public Guid InvestorId { get; set; }
        public Investor Investor { get; set; }

        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}