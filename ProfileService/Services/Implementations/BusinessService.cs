using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Business;
using ProfileService.Contracts.Business.Address;
using ProfileService.Contracts.Business.Contact;
using ProfileService.Contracts.Business.Interest;
using ProfileService.Contracts.Business.Need;
using ProfileService.Contracts.Business.Product;
using ProfileService.Contracts.Business.Role;
using ProfileService.Models.Business;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _repository;
        private readonly ILookupInterestRepository _interestRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BusinessService> _logger;

        public BusinessService(IBusinessRepository repository, IMapper mapper, ILogger<BusinessService> logger, ILookupInterestRepository interestRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _interestRepository = interestRepository;
        }

        public async Task<ICollection<GetBusiness>> SearchAsync(SearchBusiness request)
        {
            var results = await _repository.SearchAsync(request);
            return _mapper.Map<List<GetBusiness>>(results);
        }
        
        public async Task<GetBusiness> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);
            var business = new GetBusiness
            {
                Name = result.Name,
                Description = result.Description,
                Category = result.Category.ToString(),
                Id = result.Id,
                Website = result.Website,
                DateCreated = result.DateCreated,
                CoverPhoto = result.CoverPhoto,
                DateOfIncorporation = result.IncorporationDate,
                NumberOfEmployees = result.EmployeeCount
                
            };
            return business;
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
                    CoverPhoto = model.CoverPhoto,
                    EmployeeCount = int.Parse(model.NumberOfEmployees),
                    IncorporationDate = model.DateOfIncorporation,
                    Category = model.Category switch
                    {
                        "1" => BusinessCategory.Fintech,
                        "2" => BusinessCategory.EdTech,
                        "3" => BusinessCategory.AgriTech,
                        "4" => BusinessCategory.LegalTech,
                        "99" => BusinessCategory.Other,
                        _ => BusinessCategory.Other
                    },
                    
                };
                await _repository.InsertAsync(business);
                _logger.LogInformation(JsonConvert.SerializeObject(business));

                var role = new BusinessRole
                {
                    BusinessId = business.Id,
                    Role = RoleType.PageAdmin,
                    PersonId = Guid.Parse(model.CreatedBy)
                };
                await _repository.AddRoleAsync(role);
                _logger.LogInformation(JsonConvert.SerializeObject(role));
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
                var business = new Business
                {    
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    Website = model.Website,
                    EmployeeCount = model.NumberOfEmployees,
                    CoverPhoto = model.CoverPhoto,
                    //IncorporationDate = Convert.ToDateTime(model.DateOfIncorporation),
                    Category = model.Category switch
                    {
                        "1" => BusinessCategory.Fintech,
                        "2" => BusinessCategory.EdTech,
                        "3" => BusinessCategory.AgriTech,
                        "4" => BusinessCategory.LegalTech,
                        "99" => BusinessCategory.Other,
                        _ => BusinessCategory.Other
                    },
                    
                };
                
                _logger.LogInformation(JsonConvert.SerializeObject(business));
                
                await _repository.UpdateAsync(business);
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

        public async Task<NewBusinessAddress> AddAddressAsync(NewBusinessAddress address)
        {
            try
            {
                var model = new BusinessAddress
                {
                    Building = address.Building,
                    City = address.City,
                    Country = address.Country,
                    Floor = address.Floor,
                    Region = address.Region,
                    Street = address.Street,
                    AddressLine = address.AddressLine,
                    PostalCode = address.PostalCode,
                    BusinessId = address.BusinessId,
                    Type = address.Type switch
                    {
                        "1" => AddressType.Mailing,
                        "2" => AddressType.Physical,
                        "3" => AddressType.Billing,
                        "99" => AddressType.Other,
                        _ => AddressType.Other
                    }
                };
                
                await _repository.AddAddressAsync(model);
                return _mapper.Map<NewBusinessAddress>(model);
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
            _logger.LogInformation(JsonConvert.SerializeObject(interests));
            
            return _mapper.Map<ICollection<GetBusinessInterest>>(interests);
        }

        public async Task<GetBusinessInterest> AddInterestAsync(NewBusinessInterest interest)
        {
            try
            {
                var interestId = interest.InterestId ?? Guid.NewGuid();
                
                if (!interest.InterestId.HasValue && !string.IsNullOrEmpty(interest.Name))
                {
                    await _interestRepository.InsertAsync(new Interest
                    {
                        Id = interestId,
                        Category = interest.Name
                    });
                }
                
                var model = new BusinessInterest
                {
                    BusinessId = interest.BusinessId,
                    InterestId = interestId
                };
                await _repository.AddInterestAsync(model);

                var r = await _interestRepository.GetByIdAsync(interestId);

                return _mapper.Map<GetBusinessInterest>(model);
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

        public async Task<GetBusinessProduct> AddProductAsync(NewBusinessProduct address)
        {
            try
            {
                var model = _mapper.Map<BusinessProduct>(address);
                await _repository.AddProductAsync(model);
                return _mapper.Map<GetBusinessProduct>(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<GetBusinessProduct> UpdateProductAsync(UpdateBusinessProduct address)
        {
            try
            {
                var model = _mapper.Map<BusinessProduct>(address);
                await _repository.UpdateProductAsync(model);

                return _mapper.Map<GetBusinessProduct>(model);
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