using System.Collections.Generic;
using System.Threading.Tasks;
using Exemplo.API.Domain.Models;
using Exemplo.API.Domain.Services.Communication;

namespace Exemplo.API.Domain.Services
{
    public interface ICategoryService
    {
         Task<IEnumerable<Category>> ListAsync();
         Task<CategoryResponse> SaveAsync(Category category);
         Task<CategoryResponse> UpdateAsync(int id, Category category);
         Task<CategoryResponse> DeleteAsync(int id);
    }
}