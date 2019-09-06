using PoC.ES.Api.Domain.Dtos.Limits;
using PoC.ES.Api.Domain.Entities.Limits;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoC.ES.Api.Domain.Repositories.Limits
{
    public interface ILimitUsedQueryRepository : IBaseQueryRepository<LimitUsed>
    {
        Task<IEnumerable<LimitLevelResumeDto>> GetResumeAsync(string companyKey, string documentNumber);
    }
}
