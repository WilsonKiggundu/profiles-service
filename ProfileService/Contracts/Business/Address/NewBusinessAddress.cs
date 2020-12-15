using System;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Business.Address
{
    /// <summary>
    /// New BusinessAddress
    /// </summary>
    public class NewBusinessAddress : BaseModel
    {
        public string Street { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public Guid BusinessId { get; set; }
        public string Type { get; set; }
    }
}