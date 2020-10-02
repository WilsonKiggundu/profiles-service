using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Business
{
    public class BusinessAddress : Address
    {
        public Guid BusinessId { get; set; }    
        public Business Business { get; set; }
    }

}