using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Business;
using ProfileService.Models.Business;
using ProfileService.Models.Common;

namespace ProfileService.Repositories.Interfaces
{
    /// <summary>
    /// Business repository
    /// </summary>
    public interface IBusinessRepository : IGenericRepository<Business>
    {
        /// <summary>
        /// Search person
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<SearchBusinessResponse> SearchAsync(SearchBusinessRequest request);

        #region Business Addresses
    
        Task<IEnumerable<BusinessAddress>> GetAddressesAsync(Guid businessId);
        Task AddAddressAsync(BusinessAddress address);
        Task UpdateAddressAsync(BusinessAddress address);    
        Task DeleteAddressAsync(Guid businessId, Guid addressId);    

        #endregion    
        
        #region Business Contacts
    
        Task<IEnumerable<Contact>> GetContactsAsync(Guid businessId);
        Task AddContactAsync(BusinessContact contact);
        Task UpdateContactAsync(BusinessContact contact);    
        Task DeleteContactAsync(Guid contactId, Guid belongsTo);    

        #endregion
        
        #region Business Interests
    
        Task<IEnumerable<BusinessInterest>> GetInterestsAsync(Guid businessId);
        Task AddInterestAsync(BusinessInterest interest);
        Task UpdateInterestAsync(BusinessInterest interest);    
        Task DeleteInterestAsync(Guid businessId, Guid interestId);    

        #endregion
        
        #region Business Needs
    
        Task<IEnumerable<BusinessNeed>> GetNeedsAsync(Guid businessId);
        Task AddNeedAsync(BusinessNeed need);
        Task UpdateNeedAsync(BusinessNeed need);    
        Task DeleteNeedAsync(Guid needId);    

        #endregion
        
        #region Business Products
        
        Task<IEnumerable<BusinessProduct>> GetProductsAsync(Guid businessId);
        Task AddProductAsync(BusinessProduct product);
        Task<BusinessProduct> UpdateProductAsync(BusinessProduct product);    
        Task DeleteProductAsync(Guid productId, Guid businessId);    

        #endregion
        
        #region Business Roles
    
        Task<IEnumerable<BusinessRole>> GetRolesAsync(Guid businessId);
        Task AddRoleAsync(BusinessRole role);
        Task UpdateRoleAsync(BusinessRole role);    
        Task DeleteRoleAsync(Guid roleId);        

        #endregion

    }
}