using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Queries.Limits;
using System;
using System.Threading.Tasks;

namespace PoC.ES.Api.Domain.Repositories.Limits
{
    public interface ICustomerQueryRepository : IBaseQueryRepository<LimitCustomer>
    {
        Task<LimitCustomerForDailyQuery> GetLimitForDaily(string companyKey, string documentNumber, DateTime day);
    }
}
