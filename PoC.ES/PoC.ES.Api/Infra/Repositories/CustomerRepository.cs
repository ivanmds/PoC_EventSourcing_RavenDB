using Newtonsoft.Json;
using PoC.ES.Api.Domain.Repositories;
using Raven.Client.Documents;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using System.Linq;

namespace PoC.ES.Api.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private DocumentStore _store;

        public CustomerRepository()
        {
          

            _serializer = _store.Conventions.CreateSerializer();
        }


        private static string NameBase => "limits";
        public static void LoadDatabase()
        {
            var store = new DocumentStore { Urls = new[] { "http://raven_db:8080" } };
            store.Initialize();

            var operation = new GetDatabaseNamesOperation(0, 100);
            var databases = store.Maintenance.Server.Send(operation);

            if (!databases.Contains(NameBase))
            {
                var createDb = new CreateDatabaseOperation(new DatabaseRecord(NameBase));
                store.Maintenance.Server.Send(createDb);
            }
        }
    }
}
