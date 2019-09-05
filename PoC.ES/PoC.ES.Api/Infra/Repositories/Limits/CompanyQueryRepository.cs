using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Repositories.Limits;

namespace PoC.ES.Api.Infra.Repositories.Limits
{
    public class CompanyQueryRepository : BaseQueryRepository<LimitCompany>, ICompanyQueryRepository
    {
        public CompanyQueryRepository(string url = null) : base(url) { }
    }
}
