using System;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Business.Contact
{
    /// <summary>
    /// Update BusinessContact
    /// </summary>
    public class UpdateBusinessContact : BaseModel
    {
        public ContactType Type { get; set; }
        public string Value { get; set; }
        public Guid BelongsTo { get; set; }
        public string Details { get; set; }
        public ContactCategory Category { get; set; }
    }
}