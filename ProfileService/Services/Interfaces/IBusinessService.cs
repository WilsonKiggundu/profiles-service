using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Business;
using ProfileService.Contracts.Business.Address;
using ProfileService.Contracts.Business.Contact;
using ProfileService.Contracts.Business.Interest;
using ProfileService.Contracts.Business.Need;
using ProfileService.Contracts.Business.Product;
using ProfileService.Contracts.Business.Role;

namespace ProfileService.Services.Interfaces
{
    public interface IBusinessService : IService
    {
        Task<ICollection<GetBusiness>> SearchAsync(SearchBusiness request);
        Task<GetBusiness> GetByIdAsync(Guid id);
        Task InsertAsync(NewBusiness business);
        Task UpdateAsync(UpdateBusiness business);
        Task DeleteAsync(Guid id);

        #region Business Addresses
    
        Task<IEnumerable<GetBusinessAddress>> GetAddressesAsync(Guid businessId);
        Task<NewBusinessAddress> AddAddressAsync(NewBusinessAddress address);
        Task UpdateAddressAsync(UpdateBusinessAddress address);    
        Task DeleteAddressAsync(Guid addressId);    

        #endregion    
        
        #region Business Contacts
    
        Task<IEnumerable<GetBusinessContact>> GetContactsAsync(Guid businessId);
        Task AddContactAsync(NewBusinessContact contact);
        Task UpdateContactAsync(UpdateBusinessContact contact);        
        Task DeleteContactAsync(Guid contactId);    

        #endregion
        
        #region Business Interests
    
        Task<IEnumerable<GetBusinessInterest>> GetInterestsAsync(Guid businessId);
        Task<GetBusinessInterest> AddInterestAsync(NewBusinessInterest interest);
        Task UpdateInterestAsync(UpdateBusinessInterest interest);    
        Task DeleteInterestAsync(Guid interestId);        

        #endregion
        
        #region Business Needs
    
        Task<IEnumerable<GetBusinessNeed>> GetNeedsAsync(Guid businessId);
        Task AddNeedAsync(NewBusinessNeed need);    
        Task UpdateNeedAsync(UpdateBusinessNeed need);    
        Task DeleteNeedAsync(Guid needId);    

        #endregion
        
        #region Business Products
        
        Task<IEnumerable<GetBusinessProduct>> GetProductsAsync(Guid businessId);
        Task<GetBusinessProduct> AddProductAsync(NewBusinessProduct product);
        Task<GetBusinessProduct> UpdateProductAsync(UpdateBusinessProduct product);    
        Task DeleteProductAsync(Guid productId);    

        #endregion
        
        #region Business Roles
    
        Task<IEnumerable<GetBusinessRole>> GetRolesAsync(Guid businessId);
        Task AddRoleAsync(NewBusinessRole role);
        Task UpdateRoleAsync(UpdateBusinessRole role);        
        Task DeleteRoleAsync(Guid roleId);        

        #endregion
    }
}