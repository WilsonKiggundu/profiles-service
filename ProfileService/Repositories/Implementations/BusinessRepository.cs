using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Contracts.Business;
using ProfileService.Data;
using ProfileService.Models.Business;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessRepository : GenericRepository<Business>, IBusinessRepository
    {
        private readonly ProfileServiceContext _context;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public BusinessRepository(ProfileServiceContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Search Business
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ICollection<Business>> SearchAsync(SearchBusiness request)
        {
            return await _context.Businesses.ToListAsync();
        }

        public async Task<IEnumerable<BusinessAddress>> GetAddressesAsync(Guid businessId)
        {
            return await _context.BusinessAddresses
                .Where(q => q.BusinessId == businessId)
                .ToListAsync();
        }

        public async Task AddAddressAsync(BusinessAddress address)
        {
            await _context.BusinessAddresses.AddAsync(address);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAddressAsync(BusinessAddress address)
        {
            _context.BusinessAddresses.Update(address);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(Guid addressId)
        {
            var address = await _context.BusinessAddresses.FirstAsync(q => q.Id == addressId);
            address.IsDeleted = true;

            _context.BusinessAddresses.Update(address);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BusinessContact>> GetContactsAsync(Guid businessId)
        {
            throw new NotImplementedException();
        }

        public async Task AddContactAsync(BusinessContact contact)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateContactAsync(BusinessContact contact)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteContactAsync(Guid contactId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BusinessInterest>> GetInterestsAsync(Guid businessId)
        {
            return await _context.BusinessInterests
                .Include(p => p.Interest)
                .Where(q => q.BusinessId == businessId)
                .ToListAsync();
        }

        public async Task AddInterestAsync(BusinessInterest interest)
        {
            await _context.BusinessInterests
                .AddAsync(interest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInterestAsync(BusinessInterest interest)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteInterestAsync(Guid interestId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BusinessNeed>> GetNeedsAsync(Guid businessId)
        {
            throw new NotImplementedException();
        }

        public async Task AddNeedAsync(BusinessNeed need)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateNeedAsync(BusinessNeed need)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteNeedAsync(Guid needId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BusinessProduct>> GetProductsAsync(Guid businessId)
        {
            return await _context.BusinessProducts.Where(q => q.BusinessId == businessId).ToListAsync();
        }

        public async Task AddProductAsync(BusinessProduct product)
        {
            await _context.BusinessProducts.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(BusinessProduct product)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BusinessRole>> GetRolesAsync(Guid businessId)
        {
            return await _context.BusinessRoles.Where(q => q.BusinessId == businessId).ToListAsync();
        }

        public async Task AddRoleAsync(BusinessRole role)
        {
            await _context.BusinessRoles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(BusinessRole role)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteRoleAsync(Guid roleId)
        {
            throw new NotImplementedException();
        }
    }
}