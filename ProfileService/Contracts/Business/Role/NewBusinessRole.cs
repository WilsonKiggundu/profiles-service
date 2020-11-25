using System;

namespace ProfileService.Contracts.Business.Role
{
    /// <summary>
    /// New BusinessRole
    /// </summary>
    public class NewBusinessRole
    {
        public Guid BusinessId { get; set; }
        public Guid ContactId { get; set; }
        public string Role { get; set; }    
    }
}