using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Business
{
    public class BusinessNeed : BaseModel
    {
        public Guid BusinessId { get; set; }
        public Business Business { get; set; }

        public Guid NeedId { get; set; }
        public Need Need { get; set; }
    }
}