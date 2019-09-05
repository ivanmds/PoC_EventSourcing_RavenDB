using PoC.ES.Api.Domain.Entities.Limits;
using Raven.Client.Documents.Indexes;

namespace PoC.ES.Api.Infra.Repositories.Limits.Indexs
{
    public class LimitCustomerForDailyQuery : AbstractIndexCreationTask<LimitCustomer>
    {
        public LimitCustomerForDailyQuery()
        {
            //Map = LimitCustomers => from customer in LimitCustomers
            //                        select new
            //                        {
                                        
            //                        };
        }
    }
}
