using System.Threading.Tasks;

namespace Exemplo.API.Domain.Repositories
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}