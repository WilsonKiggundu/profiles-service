using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Preferences
{
    public class PostBlacklist : BaseModel
    {
        public Guid PersonId { get; set; }
        public Guid BlacklistId { get; set; }
        public string Remarks { get; set; }
    }
}