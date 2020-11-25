using System;
using System.Collections.Generic;
using ProfileService.Contracts.Lookup.Upload;
using ProfileService.Models.Common;

namespace ProfileService.Contracts.Business.Product
{
    /// <summary>
    /// Get a BusinessProduct
    /// </summary>
    public class GetBusinessProduct : BaseModel
    {  
        public Guid BusinessId { get; set; }  
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<GetLookupUpload> Uploads { get; set; }
    }
}