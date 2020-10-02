using System;

namespace ProfileService.Models.Common
{
    public class Need : BaseModel
    {
        public string Category { get; set; }
        
        public Guid? UploadId { get; set; }
        public Upload Upload { get; set; }
    }
}