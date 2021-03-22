using System;
using ProfileService.Contracts.Contact;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Person.Contact
{
    /// <summary>
    /// New PersonContact
    /// </summary>
    public class NewPersonContact : BaseModel
    {
        public ContactType Type { get; set; }
        public string Value { get; set; }
        public Guid BelongsTo { get; set; }
        public string Details { get; set; }
        public ContactCategory Category { get; set; } = ContactCategory.Primary;
    }
}