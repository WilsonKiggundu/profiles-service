using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Contracts.Lookup.Upload
{
    public class UpdateLookupUpload
    {
        [Required]
        public Guid Id { get; set; }    
        
        [Required]
        public Guid OwnerId { get; set; }
        
        [Required]
        public string FileName { get; set; }
        
        [Required]
        public int FileSize { get; set; }
        
        [Required]
        public string Path { get; set; }
        
        [Required]
        public string ContentType { get; set; }
    }
}