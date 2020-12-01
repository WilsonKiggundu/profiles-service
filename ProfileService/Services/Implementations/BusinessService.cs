using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.Contracts.Business;
using ProfileService.Contracts.Business.Address;
using ProfileService.Contracts.Business.Contact;
using ProfileService.Contracts.Business.Interest;
using ProfileService.Contracts.Business.Need;
using ProfileService.Contracts.Business.Product;
using ProfileService.Contracts.Business.Role;
using ProfileService.Models.Business;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _repository;
        private readonly IMapper _mapper;

        public BusinessService(IBusinessRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetBusiness>> SearchAsync(SearchBusiness request)
        {
            var results = await _repository.SearchAsync(request);
            return _mapper.Map<List<GetBusiness>>(results);
        }
        
        public async Task<GetBusiness> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetBusiness>(result);
        }

        public async Task InsertAsync(NewBusiness model)
        {
            try
            {
                var business = new Business
                {    
                    Name = model.Name,
                    Description = model.Description,
                    Website = model.Website,
                    EmployeeCount = int.Parse(model.NumberOfEmployees),
                    IncorporationDate = Convert.ToDateTime(model.DateOfIncorporation),
                    Category = model.Category switch
                    {
                        "1" => BusinessCategory.Fintech,
                        "2" => BusinessCategory.EdTech,
                        "3" => BusinessCategory.AgriTech,
                        "4" => BusinessCategory.LegalTech,
                        _ => BusinessCategory.Other
                    }
                };
                await _repository.InsertAsync(business);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateAsync(UpdateBusiness model)
        {
            try
            {
                var business = _mapper.Map<Business>(model);
                await _repository.InsertAsync(business);
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

        public async Task<IEnumerable<GetBusinessAddress>> GetAddressesAsync(Guid businessId)
        {
            var addresses = await _repository.GetAddressesAsync(businessId);
            return _mapper.Map<IEnumerable<GetBusinessAddress>>(addresses);
        }

        public async Task AddAddressAsync(NewBusinessAddress address)
        {
            try
            {
                var model = _mapper.Map<BusinessAddress>(address);
                await _repository.AddAddressAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateAddressAsync(UpdateBusinessAddress address)
        {
            try
            {
                var model = _mapper.Map<BusinessAddress>(address);
                await _repository.UpdateAddressAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteAddressAsync(Guid addressId)
        {
            try
            {
                await _repository.DeleteAddressAsync(addressId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<GetBusinessContact>> GetContactsAsync(Guid businessId)
        {
            var contacts = await _repository.GetContactsAsync(businessId);
            return _mapper.Map<IEnumerable<GetBusinessContact>>(contacts);
        }

        public async Task AddContactAsync(NewBusinessContact contact)
        {
            try
            {
                var model = _mapper.Map<BusinessContact>(contact);
                await _repository.AddContactAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateContactAsync(UpdateBusinessContact contact)
        {
            try
            {
                var model = _mapper.Map<BusinessContact>(contact);
                await _repository.UpdateContactAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteContactAsync(Guid contactId)
        {
            try
            {
                await _repository.DeleteContactAsync(contactId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<GetBusinessInterest>> GetInterestsAsync(Guid businessId)
        {
            var interests = await _repository.GetInterestsAsync(businessId);
            return _mapper.Map<IEnumerable<GetBusinessInterest>>(interests);
        }

        public async Task AddInterestAsync(NewBusinessInterest interest)
        {
            try
            {
                var model = _mapper.Map<BusinessInterest>(interest);
                await _repository.AddInterestAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateInterestAsync(UpdateBusinessInterest interest)
        {
            try
            {
                var model = _mapper.Map<BusinessInterest>(interest);
                await _repository.UpdateInterestAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteInterestAsync(Guid interestId)
        {
            try
            {
                await _repository.DeleteInterestAsync(interestId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<GetBusinessNeed>> GetNeedsAsync(Guid businessId)
        {
            var needs = await _repository.GetNeedsAsync(businessId);
            return _mapper.Map<IEnumerable<GetBusinessNeed>>(needs);
        }

        public async Task AddNeedAsync(NewBusinessNeed need)
        {
            try
            {
                var model = _mapper.Map<BusinessNeed>(need);
                await _repository.AddNeedAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateNeedAsync(UpdateBusinessNeed need)
        {
            try
            {
                var model = _mapper.Map<BusinessNeed>(need);
                await _repository.UpdateNeedAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteNeedAsync(Guid needId)
        {
            try
            {
                await _repository.DeleteNeedAsync(needId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<GetBusinessProduct>> GetProductsAsync(Guid businessId)
        {
            var products = await _repository.GetProductsAsync(businessId);
            return _mapper.Map<IEnumerable<GetBusinessProduct>>(products);
        }

        public async Task AddProductAsync(NewBusinessProduct address)
        {
            try
            {
                var model = _mapper.Map<BusinessProduct>(address);
                await _repository.AddProductAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateProductAsync(UpdateBusinessProduct address)
        {
            try
            {
                var model = _mapper.Map<BusinessProduct>(address);
                await _repository.UpdateProductAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteProductAsync(Guid addressId)
        {
            try
            {
                await _repository.DeleteProductAsync(addressId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<GetBusinessRole>> GetRolesAsync(Guid businessId)
        {
            var roles = await _repository.GetRolesAsync(businessId);
            return _mapper.Map<IEnumerable<GetBusinessRole>>(roles);
        }

        public async Task AddRoleAsync(NewBusinessRole role)
        {
            try
            {
                var model = _mapper.Map<BusinessRole>(role);
                await _repository.AddRoleAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateRoleAsync(UpdateBusinessRole role)
        {
            try
            {
                var model = _mapper.Map<BusinessRole>(role);
                await _repository.UpdateRoleAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteRoleAsync(Guid roleId)
        {
            try
            {
                await _repository.DeleteRoleAsync(roleId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}