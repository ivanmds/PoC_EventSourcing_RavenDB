using PoC.ES.Api.Domain.Entities;
using PoC.ES.Api.Domain.Repositories.Limits;

namespace PoC.ES.Api.Infra.Repositories.Limits
{
    public class LimitUsedQueryRepository: BaseQueryRepository<LimitUsed>, ILimitUsedQueryRepository
    {
        public LimitUsedQueryRepository(string url = null) : base(url) { }
    }
}
