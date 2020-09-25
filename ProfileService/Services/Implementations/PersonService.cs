using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileService.Contracts.Person;
using ProfileService.Models;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Search persons
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ICollection<GetPerson>> SearchAsync(SearchPerson request)
        {
            var results = await _repository.SearchAsync(request);
            return results.Select(q => new GetPerson
            {
                Id = q.Id,
                UserId = q.UserId
            }).ToList();
        }
        
        /// <summary>
        /// Get person by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetPerson> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);
            
            var person = new GetPerson
            {
                Id = result.Id,
                UserId = result.UserId
            };
            
            return person;
        }

        /// <summary>
        /// Insert person
        /// </summary>
        /// <param name="newPerson"></param>
        /// <returns></returns>
        public async Task InsertAsync(NewPerson newPerson)
        {
            var person = new Person();
            try
            {
                await _repository.InsertAsync(person);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// Update person
        /// </summary>
        /// <param name="updatePerson"></param>
        /// <returns></returns>
        public async Task UpdateAsync(UpdatePerson updatePerson)
        {
            var person = new Person();
            try
            {
                await _repository.UpdateAsync(person);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// Delete person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}