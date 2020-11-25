using System;

namespace ProfileService.Contracts.Business.Role
{
    /// <summary>
    /// Update BusinessRole
    /// </summary>
    public class UpdateBusinessRole
    {
        public Guid Id { get; set; }    
        public Guid BusinessId { get; set; }
        public Guid ContactId { get; set; }
        public string Role { get; set; }
    }
}