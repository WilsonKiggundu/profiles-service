#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class LookupStackService : ILookupStackService
    {
        private readonly ILookupStackRepository _repository;

        public LookupStackService(ILookupStackRepository repository)
        {
            _repository = repository;    
        }

        public async Task<ICollection<TechStack>> SearchAsync(string name)
        {
            return await _repository.SearchAsync(name);
        }


        public async Task<TechStack> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task InsertAsync(TechStack stack)    
        {
            try
            {
                await _repository.InsertAsync(stack);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        public async Task InsertManyAsync(ICollection<TechStack> stack)
        {
            try
            {
                await _repository.InsertManyAsync(stack);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateAsync(TechStack stack)
        {
            try
            {    
                await _repository.UpdateAsync(stack);
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