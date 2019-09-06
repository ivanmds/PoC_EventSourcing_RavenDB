using PoC.ES.Api.Domain.Entities.Limits;
using Raven.Client.Documents.Indexes;
using System.Collections.Generic;
using System.Linq;

namespace PoC.ES.Api.Infra.Repositories.Limits.Indexs
{
    public class LimitCustomerQueryIndex : AbstractIndexCreationTask<LimitCustomer, LimitCustomerQueryIndex.Reuslt>
    {
        public class Reuslt
        {
            public string Id { get; set; }
            public IEnumerable<Limit> CustomerLimits { get; set; }
            public IEnumerable<Limit> CompanyLimits { get; set; }

            public LimitLevel LimitLevel { get; set; }
        }

        public LimitCustomerQueryIndex()
        {
            Map = limitCustomers => from customer in limitCustomers

                                    select new
                                    {
                                        Id = customer.Id,
                                        CustomerLimits = customer.Limits
                                    };

            //Reduce = results => from result in results
            //                    group result by result.Id into g
            //                    let company = LoadDocument<LimitCompany>(g.Key)
            //                    select new
            //                    {
            //                        Id = g.Key,
            //                        CustomerLimits = g.Sum(p => p.CompanyLimits)
            //                        //CompanyLimits = result.CompanyLimits,
            //                        //LimitLevel = limitLevelCashInTedDailyAccount
            //                    };

        }
    }
}
