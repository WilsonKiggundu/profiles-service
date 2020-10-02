using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Investor;
using ProfileService.Models.Investor;

namespace ProfileService.Repositories.Interfaces
{
    /// <summary>
    /// Investor repository
    /// </summary>
    public interface IInvestorRepository : IGenericRepository<Investor>
    {
        /// <summary>
        /// Search person
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ICollection<Investor>> SearchAsync(SearchInvestor request);
        
        #region Investor Addresses
    
        Task<IEnumerable<InvestorAddress>> GetAddressesAsync(Guid investorId);
        Task AddAddressAsync(InvestorAddress address);
        Task UpdateAddressAsync(InvestorAddress address);    
        Task DeleteAddressAsync(Guid addressId);    

        #endregion    
        
        #region Investor Contacts
    
        Task<IEnumerable<InvestorContact>> GetContactsAsync(Guid investorId);
        Task AddContactAsync(InvestorContact contact);
        Task UpdateContactAsync(InvestorContact contact);    
        Task DeleteContactAsync(Guid contactId);    

        #endregion
        
        #region Investor Interests
    
        Task<IEnumerable<InvestorInterest>> GetInterestsAsync(Guid investorId);
        Task AddInterestAsync(InvestorInterest interest);
        Task UpdateInterestAsync(InvestorInterest interest);    
        Task DeleteInterestAsync(Guid interestId);    

        #endregion
            
        #region Investor Portfolio
    
        Task<IEnumerable<InvestorPortfolio>> GetPortfoliosAsync(Guid investorId);
        Task AddPortfolioAsync(InvestorPortfolio portfolio);
        Task UpdatePortfolioAsync(InvestorPortfolio portfolio);    
        Task DeletePortfolioAsync(Guid interestId);    

        #endregion
    }
}