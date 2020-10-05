using System;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Lookup.Category
{
    public class NewLookupCategory
    {
        public string Name { get; set; }
        public Guid? IconId { get; set; }
    }
}