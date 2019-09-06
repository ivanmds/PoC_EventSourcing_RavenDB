using System.Threading.Tasks;
using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Repositories.Limits;

namespace PoC.ES.Api.Domain.Services.Limits
{
    public class LimitService : ILimitService
    {
        private readonly ICompanyQueryRepository _companyQuery;
        private readonly ICustomerQueryRepository _customerQuery;

        public LimitService(ICompanyQueryRepository companyQuery, ICustomerQueryRepository customerQuery)
        {
            _companyQuery = companyQuery;
            _customerQuery = customerQuery;
        }

        public async Task<LimitCustomer> GetLimitAsync(string companyKey, string documentNumber)
        {
            var limitCompany = await _companyQuery.GetAsync(companyKey);
            var limitCustomer = await _customerQuery.GetAsync(LimitCustomer.GetId(companyKey, documentNumber));

            if (limitCustomer is null) limitCustomer = LimitCustomer.Create(companyKey, documentNumber);

            foreach (var limit in limitCompany.Limits)
                foreach (var cycle in limit.Cycles)
                    foreach (var limitLevel in cycle.LimitLevels)
                    {
                        if (limitCustomer.NotHasLimitLevel(limit.Type, limit.FeatureType, cycle.Type, limitLevel.Type))
                            limitCustomer.AddLimitLevel(limit.Type, limit.FeatureType, cycle.Type, limitLevel);
                    }

            return limitCustomer;
        }
    }
}
