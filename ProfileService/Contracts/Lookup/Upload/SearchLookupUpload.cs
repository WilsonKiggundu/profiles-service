using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Contracts.Lookup.Upload
{
    public class SearchLookupUpload
    {
        public Guid? OwnerId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}