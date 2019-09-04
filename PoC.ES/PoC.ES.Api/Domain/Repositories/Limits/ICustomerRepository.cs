using PoC.ES.Api.Domain.Entities.Limits;

namespace PoC.ES.Api.Domain.Repositories.Limits
{
    public interface ICustomerRepository: IBaseRepository<Customer>
    {
        void AddLimit(string aggregateRootId, Limit limit);
    }
}
