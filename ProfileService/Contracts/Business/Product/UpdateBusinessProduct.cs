using System;
using System.Collections.Generic;
using ProfileService.Contracts.Lookup.Upload;

namespace ProfileService.Contracts.Business.Product
{
    /// <summary>
    /// Update BusinessProduct
    /// </summary>
    public class UpdateBusinessProduct    
    {
        public Guid Id { get; set; }
        public Guid BusinessId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<NewLookupUpload> Uploads { get; set; }
    }
}