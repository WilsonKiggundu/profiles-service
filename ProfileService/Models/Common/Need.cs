using System;

namespace ProfileService.Models.Common
{
    public class Need : BaseModel
    {
        public string Category { get; set; }
        
        public Guid? IconId { get; set; }    
        public Upload Icon { get; set; }
    }
}