using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Investor;
using ProfileService.Contracts.Investor.Address;
using ProfileService.Contracts.Investor.Contact;
using ProfileService.Contracts.Investor.Interest;
using ProfileService.Contracts.Investor.Portfolio;
using ProfileService.Models.Investor;

namespace ProfileService.Services.Interfaces
{
    public interface IInvestorService : IService
    {
        Task<ICollection<GetInvestor>> SearchAsync(SearchInvestor request);
        Task<GetInvestor> GetByIdAsync(Guid id);
        Task InsertAsync(NewInvestor investor);
        Task UpdateAsync(UpdateInvestor investor);
        Task DeleteAsync(Guid id);
        
        #region Investor Addresses
    
        Task<IEnumerable<GetInvestorAddress>> GetAddressesAsync(Guid investorId);
        Task AddAddressAsync(NewInvestorAddress address);    
        Task UpdateAddressAsync(UpdateInvestorAddress address);    
        Task DeleteAddressAsync(Guid addressId);    

        #endregion    
        
        #region Investor Contacts
    
        Task<IEnumerable<GetInvestorContact>> GetContactsAsync(Guid investorId);
        Task AddContactAsync(NewInvestorContact contact);
        Task UpdateContactAsync(UpdateInvestorContact contact);    
        Task DeleteContactAsync(Guid contactId);    

        #endregion
        
        #region Investor Interests
    
        Task<IEnumerable<GetInvestorInterest>> GetInterestsAsync(Guid investorId);
        Task AddInterestAsync(NewInvestorInterest interest);
        Task UpdateInterestAsync(UpdateInvestorInterest interest);    
        Task DeleteInterestAsync(Guid interestId);    

        #endregion
            
        #region Investor Portfolio
        
        Task<IEnumerable<GetInvestorPortfolio>> GetPortfoliosAsync(Guid investorId);
        Task AddPortfolioAsync(NewInvestorPortfolio portfolio);
        Task UpdatePortfolioAsync(UpdateInvestorPortfolio portfolio);    
        Task DeletePortfolioAsync(Guid interestId);    

        #endregion
    }
}