using System;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Business.Need
{
    /// <summary>
    /// Get a BusinessNeed
    /// </summary>
    public class GetBusinessNeed : BaseModel
    {
        public Guid BusinessId { get; set; }
        public string Details { get; set; }    
    }
}