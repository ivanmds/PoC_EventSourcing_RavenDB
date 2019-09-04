using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Repositories.Limits;

namespace PoC.ES.Api.Infra.Repositories.Limits
{
    public class CompanyRepository : BaseRepository<LimitCompany>, ICompanyRepository
    {
        public CompanyRepository() { }
        public CompanyRepository(string url) : base(url) { }
    }
}
