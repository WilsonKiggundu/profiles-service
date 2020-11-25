using System;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Business.Address
{
    /// <summary>
    /// Update BusinessAddress
    /// </summary>
    public class UpdateBusinessAddress : BaseModel
    {
        /// <summary>
        /// Street address
        /// </summary>
        public string Street { get; set; }
        
        /// <summary>
        /// Address line 2
        /// </summary>
        public string AddressLine2 { get; set; }
        
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// State / Province / Region
        /// </summary>
        public string Region { get; set; }
        
        /// <summary>
        /// Postal code
        /// </summary>
        public string PostalCode { get; set; }
        
        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Business Id
        /// </summary>
        public Guid BusinessId { get; set; }    
        
    }
}