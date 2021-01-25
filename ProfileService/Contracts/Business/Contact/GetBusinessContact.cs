using System;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Business.Contact
{
    /// <summary>
    /// Get a BusinessContact
    /// </summary>
    public class GetBusinessContact : BaseModel
    {
        public ContactType Type { get; set; }
        public string Value { get; set; }
        public Guid BelongsTo { get; set; }
        public string Details { get; set; }
        public ContactCategory Category { get; set; }
    }
}