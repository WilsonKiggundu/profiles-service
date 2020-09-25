using System;

namespace ProfileService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime DateCreated { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DateLastUpdated { get; set; }
    }
}