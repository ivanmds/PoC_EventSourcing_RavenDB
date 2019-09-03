using PoC.ES.Api.Domain.Entities;
using PoC.ES.Api.Domain.Repositories;

namespace PoC.ES.Api.Infra.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository() { }
        public CustomerRepository(string url) :base(url) { } 
    }
}
