using PoC.ES.Api.Domain.Entities;
using System.Threading.Tasks;

namespace PoC.ES.Api.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : AggregateRoot
    {
        Task AddOrUpdateAsync(TEntity entity);
        Task<TEntity> GetAsync(string aggregateRootId);
    }
}
