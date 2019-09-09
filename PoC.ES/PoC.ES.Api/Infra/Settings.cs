using PoC.ES.Api.Infra.Repositories.Limits.Indexs;
using Raven.Client.Documents;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using System;
using System.Linq;

namespace PoC.ES.Api.Infra
{
    public static class Settings
    {
        public static string DataBase => "scouter";

        public static void LoadDatabase(string url = null)
        {
            if (url is null) url = "http://0.0.0.0:8080";

            var store = new DocumentStore { Urls = new[] { url }, Database = DataBase };
            store.Initialize();

            var operation = new GetDatabaseNamesOperation(0, 100);
            var databases = store.Maintenance.Server.Send(operation);

            if (!databases.Contains(DataBase))
            {
                var createDb = new CreateDatabaseOperation(new DatabaseRecord(DataBase));
                store.Maintenance.Server.Send(createDb);
            }

            new LimitUsedIndex().Execute(store);
        }
    }
}
