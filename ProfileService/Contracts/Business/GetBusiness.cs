using System.Collections.Generic;
using ProfileService.Contracts.Business.Address;
using ProfileService.Contracts.Business.Contact;
using ProfileService.Contracts.Business.Product;
using ProfileService.Contracts.Business.Role;
using ProfileService.Contracts.Lookup.Interest;
using ProfileService.Models.Business;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Business
{
    /// <summary>
    /// Get a Business
    /// </summary>
    public class GetBusiness : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmployeeCount { get; set; }
        public string IncorporationDate { get; set; }
        public string Website { get; set; }
        public string CoverPhoto { get; set; }
        public string Avatar { get; set; }
        public string Category { get; set; }
        public ICollection<GetLookupInterest> Interests { get; set; }
        public ICollection<GetBusinessAddress> Addresses { get; set; }
        public ICollection<GetBusinessProduct> Products { get; set; }
        public ICollection<GetBusinessRole> Roles { get; set; }
        public ICollection<GetBusinessContact> Contacts { get; set; }
    }
}