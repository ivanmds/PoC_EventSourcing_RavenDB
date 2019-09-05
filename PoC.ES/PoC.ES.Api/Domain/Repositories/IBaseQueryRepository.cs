using PoC.ES.Api.Domain.Entities;
using System.Threading.Tasks;

namespace PoC.ES.Api.Domain.Repositories
{
    public interface IBaseQueryRepository<TEntity> where TEntity : AggregateRoot
    {
        Task<TEntity> GetAsync(string aggregateRootId);
    }
}
