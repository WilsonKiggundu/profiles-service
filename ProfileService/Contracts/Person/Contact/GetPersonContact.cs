using System;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Person.Contact
{
    /// <summary>
    /// Get a PersonContact
    /// </summary>
    public class GetPersonContact : BaseModel
    {
        public ContactType Type { get; set; }
        public string Value { get; set; }
        public Guid BelongsTo { get; set; }
        public string Details { get; set; }
        public ContactCategory Category { get; set; }
    }
}