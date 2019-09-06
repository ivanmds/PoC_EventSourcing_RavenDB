using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Repositories.Limits;

namespace PoC.ES.Api.Infra.Repositories.Limits
{
    public class CustomerQueryRepository : BaseQueryRepository<LimitCustomer>, ICustomerQueryRepository
    {
        public CustomerQueryRepository(string url = null) : base(url) { }
    }
}
