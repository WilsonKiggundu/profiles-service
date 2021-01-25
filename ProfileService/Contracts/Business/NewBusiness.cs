using ProfileService.Models.Common;

namespace ProfileService.Contracts.Business
{
    /// <summary>
    /// New Business
    /// </summary>
    public class NewBusiness : BaseModel
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
        public string EmployeeCount { get; set; }
        
        /// <summary>
        /// Date of incorporation   
        /// </summary>
        public string IncorporationDate { get; set; }
        
        /// <summary>
        /// Website url
        /// </summary>
        public string Website { get; set; }
        
        public string Category { get; set; }    
        public string CoverPhoto { get; set; }
        public string Avatar { get; set; }

        public string CreatedBy { get; set; }
    }
}