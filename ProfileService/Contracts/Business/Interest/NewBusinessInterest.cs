using System;

namespace ProfileService.Contracts.Business.Interest
{
    /// <summary>
    /// New BusinessInterest
    /// </summary>
    public class NewBusinessInterest
    {
        public Guid? InterestId { get; set; }
        public Guid BusinessId { get; set; } 
        public string Name { get; set; }
    }
}