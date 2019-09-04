using PoC.ES.Api.Domain.Entities;

namespace PoC.ES.Api.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : AggregateRoot
    {
        void AddOrUpdate(TEntity entity);
        TEntity Get(string aggregateRootId);
    }
}
