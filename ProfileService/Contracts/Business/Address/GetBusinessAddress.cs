using System;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Business.Address
{
    /// <summary>
    /// Get a Business Address
    /// </summary>
    public class GetBusinessAddress : BaseModel
    {
        public string Street { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Type { get; set; }
        public Guid BusinessId { get; set; }    
    }
}