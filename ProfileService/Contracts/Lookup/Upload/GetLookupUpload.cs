using System;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Lookup.Upload
{
    public class GetLookupUpload
    {
        public Guid OwnerId { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
    }
}