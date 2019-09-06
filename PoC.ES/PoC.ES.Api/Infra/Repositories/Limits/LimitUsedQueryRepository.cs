using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoC.ES.Api.Domain.Dtos.Limits;
using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Repositories.Limits;
using PoC.ES.Api.Infra.Repositories.Limits.Indexs;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;

namespace PoC.ES.Api.Infra.Repositories.Limits
{
    public class LimitUsedQueryRepository : BaseQueryRepository<LimitUsed>, ILimitUsedQueryRepository
    {
        public LimitUsedQueryRepository(string url = null) : base(url) { }

        public async Task<IEnumerable<LimitLevelResumeDto>> GetResumeAsync(string companyKey, string documentNumber)
        {
            using (var session = Store.OpenAsyncSession())
            {
                return await session.Query<LimitUsedIndex.Reuslt, LimitUsedIndex>()
                    .Where(p => p.CompanyKey == companyKey && p.DocumentNumber == documentNumber)
                    .OfType<LimitLevelResumeDto>()
                    .ToListAsync();
            }
        }
    }
}
