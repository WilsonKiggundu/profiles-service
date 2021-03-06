using System;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Business
{
    /// <summary>
    /// Update Business
    /// </summary>
    public class UpdateBusiness : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmployeeCount { get; set; }
        public string IncorporationDate { get; set; }
        public string Website { get; set; }
        public string CoverPhoto { get; set; }    
        public string Avatar { get; set; }
        public string Category { get; set; }
    }
}