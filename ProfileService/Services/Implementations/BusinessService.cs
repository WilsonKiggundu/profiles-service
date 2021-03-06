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
        private readonly IContactRepository _contactRepository;
        private readonly ILookupInterestRepository _interestRepository;
        private readonly ILookupNeedRepository _needRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BusinessService> _logger;
        private readonly IPersonRepository _personRepository;

        public BusinessService(IBusinessRepository repository, IMapper mapper, ILogger<BusinessService> logger, ILookupInterestRepository interestRepository, IContactRepository contactRepository, IPersonRepository personRepository, ILookupNeedRepository needRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _interestRepository = interestRepository;
            _contactRepository = contactRepository;
            _personRepository = personRepository;
            _needRepository = needRepository;
        }

        public async Task<SearchBusinessResponse> SearchAsync(SearchBusinessRequest request)
        {
            return await _repository.SearchAsync(request);
        }
        
        public async Task<GetBusiness> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);

            if (result == null) return null;
            
            var business = new GetBusiness
            {
                Name = result.Name,
                Description = result.Description,
                Category = result.Category,
                Id = result.Id,
                Website = result.Website,
                DateCreated = result.DateCreated,
                CoverPhoto = result.CoverPhoto,
                Avatar = result.Avatar,
                IncorporationDate = result.IncorporationDate,
                EmployeeCount = result.EmployeeCount
                
            };
            return business;
        }

        public async Task<NewBusiness> InsertAsync(NewBusiness model)
        {
            try
            {
                var isIdPresent = !string.IsNullOrWhiteSpace(model.Id.ToString());
                
                var business = new Business
                {    
                    Id = isIdPresent ? model.Id : Guid.NewGuid(),
                    Name = model.Name,
                    Description = model.Description,
                    Website = model.Website,
                    CoverPhoto = model.CoverPhoto,
                    Avatar = model.Avatar,
                    EmployeeCount = model.EmployeeCount,
                    IncorporationDate = model.IncorporationDate,
                    Category = model.Category,
                    
                };
                await _repository.InsertAsync(business);

                var role = new BusinessRole
                {
                    BusinessId = business.Id,
                    Role = RoleType.PageAdmin.ToString(),
                    PersonId = Guid.Parse(model.CreatedBy)
                };
                
                await _repository.AddRoleAsync(role);

                return _mapper.Map<NewBusiness>(business);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<UpdateBusiness> UpdateAsync(UpdateBusiness model)
        {
            try
            {
                var business = await _repository.GetByIdAsync(model.Id);
                business.CoverPhoto = model.CoverPhoto;
                business.Name = model.Name;
                business.Description = model.Description;
                business.Website = model.Website;
                business.EmployeeCount = model.EmployeeCount;
                business.Avatar = model.Avatar;
                business.IncorporationDate = model.IncorporationDate;
                business.Category = model.Category;
                
                await _repository.UpdateAsync(business);

                return _mapper.Map<UpdateBusiness>(business);
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
                var model = _mapper.Map<BusinessAddress>(address);
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

        public async Task DeleteAddressAsync(Guid businessId, Guid addressId)
        {
            try
            {
                await _repository.DeleteAddressAsync(businessId, addressId);
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

        public async Task<BusinessContact> AddContactAsync(NewBusinessContact contact)
        {
            try
            {
                var contactId = Guid.NewGuid();
                
                var businessContact = new BusinessContact
                {
                    BusinessId = contact.BelongsTo,
                    ContactId = contactId,
                    Contact = new Contact
                    {
                        Id = contactId,
                        Category = contact.Category,
                        Details = contact.Details,
                        Type = contact.Type,
                        Value = contact.Value,
                        BelongsTo = contact.BelongsTo,
                    }
                };

                await _repository.AddContactAsync(businessContact);
                
                return businessContact;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<BusinessContact> UpdateContactAsync(UpdateBusinessContact contact)
        {
            try
            {
                _logger.LogInformation(JsonConvert.SerializeObject(contact, Formatting.Indented));
                
                var contactEntity = await _contactRepository.GetByIdAsync(contact.Id);

                contactEntity.Category = contact.Category;
                contactEntity.Details = contact.Details;
                contactEntity.Type = contact.Type;
                // contactEntity.BelongsTo = contact.BelongsTo;
                contactEntity.Value = contact.Value;
                
                await _contactRepository.UpdateAsync(contactEntity);
                
                return new BusinessContact
                {
                    BusinessId = contact.BelongsTo,
                    ContactId = contact.Id,
                    Contact = new Contact
                    {
                        Category = contact.Category,
                        Details = contact.Details,
                        Id = contact.Id,
                        Type = contact.Type,
                        Value = contact.Value,
                        BelongsTo = contact.BelongsTo
                    }
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteContactAsync(Guid contactId, Guid belongsTo)
        {
            try
            {
                await _repository.DeleteContactAsync(contactId, belongsTo);
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

        public async Task<GetBusinessInterest> AddInterestAsync(NewBusinessInterest interest, Guid businessId)    
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
                    BusinessId = businessId,
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

        public async Task DeleteInterestAsync(Guid businessId, Guid interestId)
        {
            try
            {
                await _repository.DeleteInterestAsync(businessId, interestId);
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
                var businessNeed = new Need
                {
                    Category = need.Details,
                    Id = Guid.NewGuid()
                };

                await _needRepository.InsertAsync(businessNeed);
                
                await _repository.AddNeedAsync(new BusinessNeed
                {
                    BusinessId = need.BusinessId,
                    NeedId = businessNeed.Id
                });
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

        public async Task DeleteProductAsync(Guid productId, Guid businessId)
        {
            try
            {
                await _repository.DeleteProductAsync(productId, businessId);
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

        public async Task<BusinessRole> AddRoleAsync(NewBusinessRole role)
        {
            try
            {
                _logger.LogInformation(JsonConvert.SerializeObject(role, Formatting.Indented));
                
                var businessRole = new BusinessRole
                {
                    BusinessId = role.BusinessId,
                    PersonId = role.PersonId,
                    Role = role.Role.Name
                };
                
                await _repository.AddRoleAsync(businessRole);
                var person = await _personRepository.GetByIdAsync(role.PersonId);
                businessRole.Person = person;

                return businessRole;
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

        public async Task DeleteRoleAsync(Guid businessId, Guid personId)
        {
            try
            {
                await _repository.DeleteRoleAsync(businessId, personId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<UpdateBusiness> UpdateCoverPhotoAsync(UpdateBusiness model)    
        {
            var business = await _repository.GetByIdAsync(model.Id);
            business.CoverPhoto = model.CoverPhoto;
            
            try
            {
                await _repository.UpdateAsync(business);
                return _mapper.Map<UpdateBusiness>(business);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<UpdateBusiness> UpdateAvatarAsync(UpdateBusiness model)    
        {
            var business = await _repository.GetByIdAsync(model.Id);
            business.Avatar = model.Avatar;
            
            try
            {
                await _repository.UpdateAsync(business);
                return _mapper.Map<UpdateBusiness>(business);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}