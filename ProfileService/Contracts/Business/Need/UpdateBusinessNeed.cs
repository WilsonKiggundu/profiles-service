using System;

namespace ProfileService.Contracts.Business.Need
{
    /// <summary>
    /// Update BusinessNeed
    /// </summary>
    public class UpdateBusinessNeed
    {
        public Guid Id { get; set; }
        public string Details { get; set; }
        public Guid BusinessId { get; set; }
    }
}