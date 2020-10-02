using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Business;
using ProfileService.Contracts.Contact;

namespace ProfileService.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IContactService : IService
    {
        /// <summary>
        /// Search Contacts
        /// </summary>
        /// <returns></returns>    
        Task<ICollection<GetContact>> SearchAsync(SearchContact request);
        
        /// <summary>
        /// Get by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GetContact> GetByIdAsync(Guid id);
        
        /// <summary>
        /// Insert 
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        Task InsertAsync(NewContact contact);
        
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        Task UpdateAsync(UpdateContact contact);
        
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
    }
}