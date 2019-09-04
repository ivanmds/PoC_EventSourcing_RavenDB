using PoC.ES.Api.Domain.Entities;
using System.Threading.Tasks;

namespace PoC.ES.Api.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : AggregateRoot
    {
        Task SaveAsync(TEntity entity);
        Task<TEntity> GetAsync(string aggregateRootId);
    }
}
