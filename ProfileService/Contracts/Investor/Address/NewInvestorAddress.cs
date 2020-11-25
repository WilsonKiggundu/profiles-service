using System;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Investor.Address
{
    /// <summary>
    /// New InvestorAddress
    /// </summary>
    public class NewInvestorAddress
    {    
        public Guid InvestorId { get; set; }
        public AddressType Type { get; set; } = AddressType.Other;
        public string Street { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}