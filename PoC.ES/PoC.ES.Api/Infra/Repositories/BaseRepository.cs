using Raven.Client.Documents;

namespace PoC.ES.Api.Infra.Repositories
{
    public abstract class BaseRepository
    {
        protected DocumentStore Store { get; private set; }

        protected BaseRepository(string url = null)
        {
            if (string.IsNullOrEmpty(url))
                url = "http://raven_db:8080";

            Store = new DocumentStore
            {
                Urls = new[] { url },
                Database = Settings.DataBase
            };

            Store.Initialize();
        }
    }
}
