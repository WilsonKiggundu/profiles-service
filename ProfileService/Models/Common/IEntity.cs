using System;

namespace ProfileService.Models.Common
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
        public string DateCreated { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string DateLastUpdated { get; set; }
    }
}