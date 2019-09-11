using PoC.ES.ConsoleApp.Indexs;
using Raven.Client.Documents;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using System.Linq;

namespace PoC.ES.ConsoleApp
{
    public static class MyStore
    {
        public static DocumentStore GetDocumentStore(string url = null)
        {
            if (url is null) url = "http://localhost:8080";

            var dataBase = "kairos";
            var store = new DocumentStore { Urls = new[] { url }, Database = dataBase };
            store.Initialize();

            var operation = new GetDatabaseNamesOperation(0, 10);
            var databases = store.Maintenance.Server.Send(operation);

            if (!databases.Contains(dataBase))
            {
                var createDb = new CreateDatabaseOperation(new DatabaseRecord(dataBase));
                store.Maintenance.Server.Send(createDb);
            }

            new ListFeedEventAggregatedIndex().Execute(store);

            return store;
        }
    }
}
