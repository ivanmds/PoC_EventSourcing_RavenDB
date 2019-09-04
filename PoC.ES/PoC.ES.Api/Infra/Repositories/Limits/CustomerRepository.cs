using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Repositories.Limits;

namespace PoC.ES.Api.Infra.Repositories.Limits
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository() { }
        public CustomerRepository(string url) :base(url) { }
    }
}
