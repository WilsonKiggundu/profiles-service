using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Contact;
using ProfileService.Models.Common;

namespace ProfileService.Repositories.Interfaces
{
    /// <summary>
    /// Contact repository
    /// </summary>
    public interface IContactRepository : IGenericRepository<Contact>
    {
        /// <summary>
        /// Search person
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ICollection<Contact>> SearchAsync(SearchContact request);
    }
}