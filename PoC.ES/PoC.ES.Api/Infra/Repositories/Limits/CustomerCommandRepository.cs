using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Repositories.Limits;

namespace PoC.ES.Api.Infra.Repositories.Limits
{
    public class CustomerCommandRepository : BaseCommandRepository<LimitCustomer>, ICustomerCommandRepository
    {
        public CustomerCommandRepository(string url = null) :base(url) { }
    }
}
