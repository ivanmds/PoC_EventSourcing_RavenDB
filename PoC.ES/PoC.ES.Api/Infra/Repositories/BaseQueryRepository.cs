using System.Threading.Tasks;
using PoC.ES.Api.Domain.Entities;
using PoC.ES.Api.Domain.Repositories;

namespace PoC.ES.Api.Infra.Repositories
{
    public abstract class BaseQueryRepository<TEntity> : BaseRepository, IBaseQueryRepository<TEntity> where TEntity : AggregateRoot
    {
        protected BaseQueryRepository(string url = null) : base(url) { }

        public async Task<TEntity> GetAsync(string id)
        {
            using (var session = Store.OpenAsyncSession())
                return await session.LoadAsync<TEntity>(id);
        }
    }
}
