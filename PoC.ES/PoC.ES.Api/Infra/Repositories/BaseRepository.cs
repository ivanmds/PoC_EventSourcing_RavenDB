using Raven.Client.Documents;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using System.Linq;

namespace PoC.ES.Api.Infra.Repositories
{
    public abstract class BaseRepository
    {
        protected DocumentStore Store { get; private set; }


        public BaseRepository()
        {
            Store = new DocumentStore
            {
                Urls = new[] { "http://raven_db:8080" },
                Database = DataBase
            };

            Store.Initialize();
        }

        private static string DataBase => "scouter";

        public static void LoadDatabase()
        {
            var store = new DocumentStore { Urls = new[] { "http://raven_db:8080" } };
            store.Initialize();

           
            var operation = new GetDatabaseNamesOperation(0, 100);
            var databases = store.Maintenance.Server.Send(operation);

            if (!databases.Contains(DataBase))
            {
                var createDb = new CreateDatabaseOperation(new DatabaseRecord(DataBase));
                store.Maintenance.Server.Send(createDb);
            }
        }
    }
}
