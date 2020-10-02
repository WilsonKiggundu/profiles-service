using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.Contracts.Lookup.Upload;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class LookupUploadService : ILookupUploadService
    {
        private readonly IMapper _mapper;
        private readonly ILookupUploadRepository _repository;

        public LookupUploadService(IMapper mapper, ILookupUploadRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ICollection<GetLookupUpload>> SearchAsync(SearchLookupUpload request)
        {
            var uploads = await _repository.SearchAsync(request);
            return _mapper.Map<ICollection<GetLookupUpload>>(uploads);
        }

        public async Task<GetLookupUpload> GetByIdAsync(Guid id)
        {
            var upload = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetLookupUpload>(upload);
        }

        public async Task InsertAsync(NewLookupUpload upload)
        {
            try
            {
                var model = _mapper.Map<Upload>(upload);
                await _repository.InsertAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateAsync(UpdateLookupUpload upload)
        {
            try
            {
                var model = _mapper.Map<Upload>(upload);
                await _repository.UpdateAsync(model);
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