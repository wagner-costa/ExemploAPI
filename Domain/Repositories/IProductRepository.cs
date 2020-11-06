using System.Collections.Generic;
using System.Threading.Tasks;
using Exemplo.API.Domain.Models;
using Exemplo.API.Domain.Models.Queries;

namespace Exemplo.API.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<QueryResult<Product>> ListAsync(ProductsQuery query);
        Task AddAsync(Product product);
        Task<Product> FindByIdAsync(int id);
        void Update(Product product);
        void Remove(Product product);
    }
}