using System;
using System.Collections.Generic;
using ProfileService.Contracts.Lookup.Upload;

namespace ProfileService.Contracts.Person.Awards
{
    public class NewPersonAward    
    {
        public Guid PersonId { get; set; }
        public string AwardedBy { get; set; }
        public string Title { get; set; }    
        public string DateOfAward { get; set; }    
        public string Category { get; set; }
        public string Description { get; set; }
        public ICollection<GetLookupUpload> Attachments { get; set; }
    }
}