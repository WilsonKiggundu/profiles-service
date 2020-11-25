using System;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Business.Role
{
    /// <summary>
    /// Get a BusinessRole
    /// </summary>
    public class GetBusinessRole : BaseModel
    {
        public Guid BusinessId { get; set; }
        public Guid ContactId { get; set; }
        public string Role { get; set; }
    }
}