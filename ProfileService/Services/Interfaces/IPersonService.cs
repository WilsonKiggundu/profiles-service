using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Person;

namespace ProfileService.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Search persons
        /// </summary>
        /// <returns></returns>    
        Task<ICollection<GetPerson>> SearchAsync(SearchPerson request);
        
        /// <summary>
        /// Get by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GetPerson> GetByIdAsync(Guid id);
        
        /// <summary>
        /// Insert 
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        Task InsertAsync(NewPerson person);
        
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        Task UpdateAsync(UpdatePerson person);
        
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
    }
}