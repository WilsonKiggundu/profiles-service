using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Business
{
    public class BusinessInterest : BaseModel
    {
        public Guid BusinessId { get; set; }
        public Business Business { get; set; }    

        public Guid InterestId { get; set; }
        public Interest Interest { get; set; }    
    }
}