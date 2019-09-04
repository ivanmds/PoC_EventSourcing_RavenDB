using PoC.ES.Api.Domain.Entities;
using PoC.ES.Api.Domain.Repositories;
using Raven.Client.Documents;

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


        public void Add(TEntity entity)
        {
            using (var session = Store.OpenSession())
            {
                session.Store(entity);
                session.SaveChanges();
            }
        }

        public TEntity Get(string id)
        {
            using (var session = Store.OpenSession())
                return session.Load<TEntity>(id);
        }
    }
}
