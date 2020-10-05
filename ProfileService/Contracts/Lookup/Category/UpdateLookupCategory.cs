using System;

namespace ProfileService.Contracts.Lookup.Category
{
    public class UpdateLookupCategory
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public Guid? IconId { get; set; }
    }
}