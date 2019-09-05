using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Repositories.Limits;

namespace PoC.ES.Api.Infra.Repositories.Limits
{
    public class CompanyCommandRepository : BaseCommandRepository<LimitCompany>, ICompanyCommandRepository
    {
        public CompanyCommandRepository(string url = null) : base(url) { }
    }
}
