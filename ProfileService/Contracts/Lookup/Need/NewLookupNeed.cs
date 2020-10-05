using System;

namespace ProfileService.Contracts.Lookup.Need
{
    public class NewLookupNeed
    {
        public string Category { get; set; }
        public Guid? IconId { get; set; }
    }
}