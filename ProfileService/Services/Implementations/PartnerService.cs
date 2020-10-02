using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.Contracts.Partner;
using ProfileService.Contracts.Partner.Address;
using ProfileService.Contracts.Partner.Contact;
using ProfileService.Contracts.Partner.Interest;
using ProfileService.Contracts.Partner.Contribution;
using ProfileService.Contracts.Partner.Portfolio;
using ProfileService.Contracts.Partner.Role;
using ProfileService.Models.Partner;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class PartnerService : IPartnerService
    {
        private readonly IPartnerRepository _repository;
        private readonly IMapper _mapper;

        public PartnerService(IPartnerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetPartner>> SearchAsync(SearchPartner request)
        {
            var results = await _repository.SearchAsync(request);
            return _mapper.Map<ICollection<GetPartner>>(results);
        }
        
        public async Task<GetPartner> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetPartner>(result);
        }

        public async Task InsertAsync(NewPartner model)
        {
            try
            {
                var business = _mapper.Map<Partner>(model);
                await _repository.InsertAsync(business);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateAsync(UpdatePartner model)
        {
            try
            {
                var business = _mapper.Map<Partner>(model);
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

        public async Task<IEnumerable<GetPartnerAddress>> GetAddressesAsync(Guid businessId)
        {
            var addresses = await _repository.GetAddressesAsync(businessId);
            return _mapper.Map<IEnumerable<GetPartnerAddress>>(addresses);
        }

        public async Task AddAddressAsync(NewPartnerAddress address)
        {
            try
            {
                var model = _mapper.Map<PartnerAddress>(address);
                await _repository.AddAddressAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateAddressAsync(UpdatePartnerAddress address)
        {
            try
            {
                var model = _mapper.Map<PartnerAddress>(address);
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

        public async Task<IEnumerable<GetPartnerContact>> GetContactsAsync(Guid businessId)
        {
            var contacts = await _repository.GetContactsAsync(businessId);
            return _mapper.Map<IEnumerable<GetPartnerContact>>(contacts);
        }

        public async Task AddContactAsync(NewPartnerContact contact)
        {
            try
            {
                var model = _mapper.Map<PartnerContact>(contact);
                await _repository.AddContactAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateContactAsync(UpdatePartnerContact contact)
        {
            try
            {
                var model = _mapper.Map<PartnerContact>(contact);
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

        public async Task<IEnumerable<GetPartnerInterest>> GetInterestsAsync(Guid businessId)
        {
            var interests = await _repository.GetInterestsAsync(businessId);
            return _mapper.Map<IEnumerable<GetPartnerInterest>>(interests);
        }

        public async Task AddInterestAsync(NewPartnerInterest interest)
        {
            try
            {
                var model = _mapper.Map<PartnerInterest>(interest);
                await _repository.AddInterestAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateInterestAsync(UpdatePartnerInterest interest)
        {
            try
            {
                var model = _mapper.Map<PartnerInterest>(interest);
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

        public async Task<IEnumerable<GetPartnerContribution>> GetContributionsAsync(Guid businessId)
        {
            var needs = await _repository.GetContributionsAsync(businessId);
            return _mapper.Map<IEnumerable<GetPartnerContribution>>(needs);
        }

        public async Task AddContributionAsync(NewPartnerContribution need)
        {
            try
            {
                var model = _mapper.Map<PartnerContribution>(need);
                await _repository.AddContributionAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateContributionAsync(UpdatePartnerContribution need)
        {
            try
            {
                var model = _mapper.Map<PartnerContribution>(need);
                await _repository.UpdateContributionAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteContributionAsync(Guid needId)
        {
            try
            {
                await _repository.DeleteContributionAsync(needId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<GetPartnerPortfolio>> GetPortfoliosAsync(Guid businessId)
        {
            var products = await _repository.GetPortfoliosAsync(businessId);
            return _mapper.Map<IEnumerable<GetPartnerPortfolio>>(products);
        }

        public async Task AddPortfolioAsync(NewPartnerPortfolio address)
        {
            try
            {
                var model = _mapper.Map<PartnerPortfolio>(address);
                await _repository.AddPortfolioAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdatePortfolioAsync(UpdatePartnerPortfolio address)
        {
            try
            {
                var model = _mapper.Map<PartnerPortfolio>(address);
                await _repository.UpdatePortfolioAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeletePortfolioAsync(Guid addressId)
        {
            try
            {
                await _repository.DeletePortfolioAsync(addressId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<GetPartnerRole>> GetRolesAsync(Guid businessId)
        {
            var roles = await _repository.GetRolesAsync(businessId);
            return _mapper.Map<IEnumerable<GetPartnerRole>>(roles);
        }

        public async Task AddRoleAsync(NewPartnerRole role)
        {
            try
            {
                var model = _mapper.Map<PartnerRole>(role);
                await _repository.AddRoleAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateRoleAsync(UpdatePartnerRole role)
        {
            try
            {
                var model = _mapper.Map<PartnerRole>(role);
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