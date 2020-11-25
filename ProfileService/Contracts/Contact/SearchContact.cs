using System;

namespace ProfileService.Contracts.Contact
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchContact
    {   
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }
        
    }
}