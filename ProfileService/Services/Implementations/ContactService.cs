using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.Contracts.Business;
using ProfileService.Contracts.Contact;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly IMapper _mapper;

        public ContactService(IContactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetContact>> SearchAsync(SearchContact request)
        {
            var results = await _repository.SearchAsync(request);
            return _mapper.Map<ICollection<GetContact>>(results);
        }
        
        public async Task<GetContact> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetContact>(result);
        }

        public async Task InsertAsync(NewContact newContact)
        {
            try
            {
                var contact = _mapper.Map<Contact>(newContact);
                await _repository.InsertAsync(contact);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateAsync(UpdateContact updateContact)
        {
            try
            {
                var contact = _mapper.Map<Contact>(updateContact);
                await _repository.UpdateAsync(contact);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

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