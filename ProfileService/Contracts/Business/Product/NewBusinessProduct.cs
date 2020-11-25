using System;
using System.Collections.Generic;
using ProfileService.Contracts.Lookup.Upload;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Business.Product
{
    /// <summary>
    /// New BusinessProduct
    /// </summary>
    public class NewBusinessProduct
    {
        public Guid BusinessId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<NewLookupUpload> Uploads { get; set; }
    }
}