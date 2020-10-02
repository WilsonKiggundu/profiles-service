using System;

namespace ProfileService.Models.Common
{
    public class Category : BaseModel
    {
        public string Name { get; set; }

        public Guid? IconId { get; set; }    
        public Upload Icon { get; set; }
    }
}