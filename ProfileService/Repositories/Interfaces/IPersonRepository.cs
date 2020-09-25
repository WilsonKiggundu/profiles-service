using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Person;
using ProfileService.Models;

namespace ProfileService.Repositories.Interfaces
{
    /// <summary>
    /// Person repository
    /// </summary>
    public interface IPersonRepository : IGenericRepository<Person>
    {
        /// <summary>
        /// Search person
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ICollection<Person>> SearchAsync(SearchPerson request);
    }
}