using System;

namespace ProfileService.Contracts.Business.Need
{
    /// <summary>
    /// New BusinessNeed
    /// </summary>
    public class NewBusinessNeed
    {
        public Guid BusinessId { get; set; }
        public string Details { get; set; }
    }
}