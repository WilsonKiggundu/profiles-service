using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Models.Common;

namespace ProfileService.Services.Interfaces
{
    public interface ILookupSchoolService : IService
    {
        Task<ICollection<School>> SearchAsync(string name);
        Task<School> GetByIdAsync(Guid id);
        Task InsertAsync(School school);
        Task InsertManyAsync(ICollection<School> schools);
        Task UpdateAsync(School school);
        Task DeleteAsync(Guid id);
    }
}