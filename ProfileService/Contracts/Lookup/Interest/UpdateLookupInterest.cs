using System;

namespace ProfileService.Contracts.Lookup.Interest
{
    public class UpdateLookupInterest
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
    }
}