using PoC.ES.Api.Domain.Entities;
using PoC.ES.Api.Domain.Repositories.Limits;

namespace PoC.ES.Api.Infra.Repositories.Limits
{
    public class LimitUsedCommandRepository : BaseCommandRepository<LimitUsed>, ILimitUsedCommandRepository
    {
        public LimitUsedCommandRepository(string url = null) : base(url) { }
    }
}
