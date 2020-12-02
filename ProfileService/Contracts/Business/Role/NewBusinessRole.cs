using System;
using ProfileService.Models.Business;

namespace ProfileService.Contracts.Business.Role
{
    /// <summary>
    /// New BusinessRole
    /// </summary>
    public class NewBusinessRole
    {
        public Guid BusinessId { get; set; }
        public Guid ContactId { get; set; }
        public RoleType Role { get; set; }    
    }
}