using System.Collections.Generic;
using ProfileService.Models.Business;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Business
{
    /// <summary>
    /// Get a Business
    /// </summary>
    public class GetBusiness : BaseModel
    {
        /// <summary>
        /// Name of the business
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Description of the business
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Number of employees
        /// </summary>
        public int? NumberOfEmployees { get; set; }
        
        /// <summary>
        /// Date of incorporation   
        /// </summary>
        public string DateOfIncorporation { get; set; }
        
        /// <summary>
        /// Website url
        /// </summary>
        public string Website { get; set; }    

        public string Category { get; set; }
    }
}