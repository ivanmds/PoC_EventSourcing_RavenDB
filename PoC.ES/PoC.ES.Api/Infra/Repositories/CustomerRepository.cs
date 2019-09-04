using PoC.ES.Api.Domain.Entities;
using PoC.ES.Api.Domain.Repositories;

namespace PoC.ES.Api.Infra.Repositories
{
    public class CustomerRepository : BaseRepository<LimitConfiguration>, ICustomerRepository
    {
        public CustomerRepository() { }
        public CustomerRepository(string url) :base(url) { }

        public void AddLimit(string cutomerId, Limit limit)
        {
            using (var session = Store.OpenSession())
            {
                var customer = session.Load<LimitConfiguration>(cutomerId);
                if (customer is null) return;

                customer.AddLimit(limit);
                session.SaveChanges();
            }
        }
    }
}
