using System.Threading.Tasks;
using Exemplo.API.Domain.Models;
using Exemplo.API.Domain.Models.Queries;
using Exemplo.API.Domain.Services.Communication;

namespace Exemplo.API.Domain.Services
{
    public interface IProductService
    {
        Task<QueryResult<Product>> ListAsync(ProductsQuery query);
        Task<ProductResponse> SaveAsync(Product product);
        Task<ProductResponse> UpdateAsync(int id, Product product);
        Task<ProductResponse> DeleteAsync(int id);
    }
}