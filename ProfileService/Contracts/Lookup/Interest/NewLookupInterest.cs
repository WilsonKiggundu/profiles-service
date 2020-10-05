using System;

namespace ProfileService.Contracts.Lookup.Interest
{
    public class NewLookupInterest
    {
        public string Category { get; set; }
        public Guid? IconId { get; set; }
    }
}