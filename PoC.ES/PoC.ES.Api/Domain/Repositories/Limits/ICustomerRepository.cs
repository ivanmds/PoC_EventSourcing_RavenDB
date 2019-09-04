using PoC.ES.Api.Domain.Entities.Limits;

namespace PoC.ES.Api.Domain.Repositories.Limits
{
    public interface ICustomerRepository: IBaseRepository<LimitConfiguration>
    {
        void AddLimit(string cutomerId, Limit limit);
    }
}
