using PoC.ES.Api.Domain.Entities;
using PoC.ES.Api.Domain.Repositories;
using Raven.Client.Documents;
using System.Threading.Tasks;

namespace PoC.ES.Api.Infra.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : AggregateRoot
    {
        protected DocumentStore Store { get; private set; }

        public BaseRepository(string url)
        {
            Store = new DocumentStore
            {
                Urls = new[] { url },
                Database = Settings.DataBase
            };

            Store.Initialize();
        }

        public BaseRepository()
            :this("http://raven_db:8080")
        {
        }


        public async Task SaveAsync(TEntity entity)
        {
            using (var session = Store.OpenAsyncSession())
            {
                await session.StoreAsync(entity);
                await session.SaveChangesAsync();
            }
        }

        public async Task<TEntity> GetAsync(string id)
        {
            using (var session = Store.OpenAsyncSession())
                return await session.LoadAsync<TEntity>(id);
        }
    }
}
