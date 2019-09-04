using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Repositories.Limits;

namespace PoC.ES.Api.Infra.Repositories.Limits
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository() { }
        public CustomerRepository(string url) :base(url) { }

        public void AddLimit(string aggregateRootId, Limit limit)
        {
            using (var session = Store.OpenSession())
            {
                var customer = session.Load<Customer>(aggregateRootId);
                if (customer is null) return;

                customer.AddLimit(limit);
                session.SaveChanges();
            }
        }
    }
}
