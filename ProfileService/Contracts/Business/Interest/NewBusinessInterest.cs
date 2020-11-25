using System;

namespace ProfileService.Contracts.Business.Interest
{
    /// <summary>
    /// New BusinessInterest
    /// </summary>
    public class NewBusinessInterest
    {
        public Guid BusinessId { get; set; } 
        public string Details { get; set; }
    }
}