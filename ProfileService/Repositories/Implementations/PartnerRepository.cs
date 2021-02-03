using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Partner;
using ProfileService.Models.Partner;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class PartnerRepository : GenericRepository<Partner>, IPartnerRepository
    {
        private readonly ProfileServiceContext _context;
        public PartnerRepository(ProfileServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<Partner>> SearchAsync(SearchPartner request)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PartnerAddress>> GetAddressesAsync(Guid partnerId)
        {
            throw new NotImplementedException();
        }

        public async Task AddAddressAsync(PartnerAddress address)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAddressAsync(PartnerAddress address)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAddressAsync(Guid addressId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PartnerContact>> GetContactsAsync(Guid partnerId)
        {
            throw new NotImplementedException();
        }

        public async Task AddContactAsync(PartnerContact contact)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateContactAsync(PartnerContact contact)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteContactAsync(Guid contactId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PartnerInterest>> GetInterestsAsync(Guid partnerId)
        {
            throw new NotImplementedException();
        }

        public async Task AddInterestAsync(PartnerInterest interest)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateInterestAsync(PartnerInterest interest)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteInterestAsync(Guid interestId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PartnerPortfolio>> GetPortfoliosAsync(Guid partnerId)
        {
            throw new NotImplementedException();
        }

        public async Task AddPortfolioAsync(PartnerPortfolio portfolio)
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePortfolioAsync(PartnerPortfolio portfolio)
        {
            throw new NotImplementedException();
        }

        public async Task DeletePortfolioAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PartnerRole>> GetRolesAsync(Guid partnerId)
        {
            throw new NotImplementedException();
        }

        public async Task AddRoleAsync(PartnerRole role)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateRoleAsync(PartnerRole role)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteRoleAsync(Guid roleId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PartnerContribution>> GetContributionsAsync(Guid partnerId)
        {
            throw new NotImplementedException();
        }

        public async Task AddContributionAsync(PartnerContribution contribution)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateContributionAsync(PartnerContribution contribution)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteContributionAsync(Guid contributionId)
        {
            throw new NotImplementedException();
        }
    }
}