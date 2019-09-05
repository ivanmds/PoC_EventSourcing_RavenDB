using System;
using System.Threading.Tasks;
using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Queries.Limits;
using PoC.ES.Api.Domain.Repositories.Limits;

namespace PoC.ES.Api.Infra.Repositories.Limits
{
    public class CustomerQueryRepository : BaseQueryRepository<LimitCustomer>, ICustomerQueryRepository
    {
        public CustomerQueryRepository(string url = null) : base(url) { }

        public Task<LimitCustomerForDailyQuery> GetLimitForDaily(string companyKey, string documentNumber, DateTime day)
        {
            throw new NotImplementedException();
        }
    }
}
