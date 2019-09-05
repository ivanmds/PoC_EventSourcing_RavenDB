using PoC.ES.Api.Domain.Entities;
using System.Threading.Tasks;

namespace PoC.ES.Api.Domain.Repositories
{
    public interface IBaseCommandRepository<TEntity> where TEntity : AggregateRoot
    {
        Task SaveAsync(TEntity entity);
    }
}
