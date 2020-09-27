using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.Contracts.Contact;
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
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public PersonService(IPersonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Search persons
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ICollection<GetPerson>> SearchAsync(SearchPerson request)
        {
            var results = await _repository.SearchAsync(request);
            return _mapper.Map<ICollection<GetPerson>>(results);
        }
        
        /// <summary>
        /// Get person by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetPerson> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetPerson>(result);
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