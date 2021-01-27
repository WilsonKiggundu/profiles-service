using System;
using System.Collections.Generic;
using ProfileService.Models.Business;

namespace ProfileService.Contracts.Business.Role
{
    /// <summary>
    /// New BusinessRole
    /// </summary>
    public class NewBusinessRole
    {
        public Guid BusinessId { get; set; }
        public Guid PersonId { get; set; }
        public RoleOption Role { get; set; }
    }

    public class RoleOption
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}