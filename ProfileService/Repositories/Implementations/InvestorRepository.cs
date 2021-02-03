using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Contracts.Investor;
using ProfileService.Models.Investor;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public class InvestorRepository : GenericRepository<Investor>, IInvestorRepository
    {
        private readonly ProfileServiceContext _context;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public InvestorRepository(ProfileServiceContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Search Investor
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ICollection<Investor>> SearchAsync(SearchInvestor request)
        {
            return await _context.Investors.ToListAsync();
        }

        public async Task<IEnumerable<InvestorAddress>> GetAddressesAsync(Guid investorId)
        {
            throw new NotImplementedException();
        }

        public async Task AddAddressAsync(InvestorAddress address)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAddressAsync(InvestorAddress address)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAddressAsync(Guid addressId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InvestorContact>> GetContactsAsync(Guid investorId)
        {
            throw new NotImplementedException();
        }

        public async Task AddContactAsync(InvestorContact contact)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateContactAsync(InvestorContact contact)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteContactAsync(Guid contactId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InvestorInterest>> GetInterestsAsync(Guid investorId)
        {
            throw new NotImplementedException();
        }

        public async Task AddInterestAsync(InvestorInterest interest)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateInterestAsync(InvestorInterest interest)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteInterestAsync(Guid interestId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InvestorPortfolio>> GetPortfoliosAsync(Guid investorId)
        {
            throw new NotImplementedException();
        }

        public async Task AddPortfolioAsync(InvestorPortfolio portfolio)
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePortfolioAsync(InvestorPortfolio portfolio)
        {
            throw new NotImplementedException();
        }

        public async Task DeletePortfolioAsync(Guid interestId)
        {
            throw new NotImplementedException();
        }
    }
}