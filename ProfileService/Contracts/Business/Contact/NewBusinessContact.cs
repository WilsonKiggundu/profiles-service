using System;
using ProfileService.Contracts.Contact;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Business.Contact
{
    /// <summary>
    /// New BusinessContact
    /// </summary>
    public class NewBusinessContact : BaseModel
    {
        public ContactType Type { get; set; }
        public string Value { get; set; }
        public Guid BelongsTo { get; set; }
        public string Details { get; set; }
        public ContactCategory Category { get; set; } = ContactCategory.Primary;
    }
}