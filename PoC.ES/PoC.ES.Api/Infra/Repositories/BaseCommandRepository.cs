using PoC.ES.Api.Domain.Entities;
using PoC.ES.Api.Domain.Repositories;
using System.Threading.Tasks;

namespace PoC.ES.Api.Infra.Repositories
{
    public abstract class BaseCommandRepository<TEntity> : BaseRepository, IBaseCommandRepository<TEntity> where TEntity : AggregateRoot
    {
        protected BaseCommandRepository(string url = null) : base(url) { }

        public async Task SaveAsync(TEntity entity)
        {
            using (var session = Store.OpenAsyncSession())
            {
                await session.StoreAsync(entity);
                await session.SaveChangesAsync();
            }
        }
    }
}
