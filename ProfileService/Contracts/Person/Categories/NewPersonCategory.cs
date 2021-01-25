using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ProfileService.Contracts.Lookup.Category;

namespace ProfileService.Contracts.Person.Categories
{
    public class NewPersonCategory
    {
        public List<CategoryViewModel> Categories { get; set; }

        public string Values { get; set; }
        public Guid PersonId { get; set; }    
    }

    public class CategoryViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}