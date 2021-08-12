using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models.Common
{
    public class Upload : BaseModel
    {
        // [Required]
        public Guid OwnerId { get; set; }

        public Guid? EntityId { get; set; }
        
        [Required]
        public string FileName { get; set; }
        
        // [Required]
        public int FileSize { get; set; }
        
        [Required]
        public string Path { get; set; }
        
        // [Required]
        public string ContentType { get; set; }
    }
}