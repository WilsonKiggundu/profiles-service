using System;

namespace ProfileService.Contracts.Business.Interest
{
    /// <summary>
    /// Update BusinessInterest
    /// </summary>
    public class UpdateBusinessInterest
    {
        public Guid Id { get; set; }    
        public Guid BusinessId { get; set; } 
        public string Details { get; set; }
    }
}