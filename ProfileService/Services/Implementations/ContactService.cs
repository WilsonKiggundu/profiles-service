using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileService.Contracts.Contact;
using ProfileService.Models;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public ContactService(IContactRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Search Contacts
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ICollection<GetContact>> SearchAsync(SearchContact request)
        {
            var results = await _repository.SearchAsync(request);
            return results.Select(q => new GetContact
            {
                Id = q.Id
            }).ToList();
        }
        
        /// <summary>
        /// Get Contact by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetContact> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);
            
            var contact = new GetContact
            {
                Id = result.Id
            };
            
            return contact;
        }

        /// <summary>
        /// Insert Contact
        /// </summary>
        /// <param name="newContact"></param>
        /// <returns></returns>
        public async Task InsertAsync(NewContact newContact)
        {
            var contact = new Contact();
            try
            {
                await _repository.InsertAsync(contact);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// Update Contact
        /// </summary>
        /// <param name="updateContact"></param>
        /// <returns></returns>
        public async Task UpdateAsync(UpdateContact updateContact)
        {
            var contact = new Contact();    
            try
            {
                await _repository.UpdateAsync(contact);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// Delete Contact
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