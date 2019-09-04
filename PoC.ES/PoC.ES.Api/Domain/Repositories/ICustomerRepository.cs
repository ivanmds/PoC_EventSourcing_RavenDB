using PoC.ES.Api.Domain.Entities;

namespace PoC.ES.Api.Domain.Repositories
{
    public interface ICustomerRepository: IBaseRepository<LimitConfiguration>
    {
        void AddLimit(string cutomerId, Limit limit);
    }
}
