using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoC.ES.Api.Domain.Dtos.Limits;
using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using PoC.ES.Api.Domain.Repositories.Limits;

namespace PoC.ES.Api.Domain.Services.Limits
{
    public class LimitService : ILimitService
    {
        private readonly ICompanyQueryRepository _companyQuery;
        private readonly ICustomerQueryRepository _customerQuery;
        private readonly ILimitUsedQueryRepository _limitUsedQuery;

        public LimitService(ICompanyQueryRepository companyQuery,
                            ICustomerQueryRepository customerQuery,
                            ILimitUsedQueryRepository limitUsedQuery)
        {
            _companyQuery = companyQuery;
            _customerQuery = customerQuery;
            _limitUsedQuery = limitUsedQuery;
        }

        public async Task<LimitCustomer> GetLimitAsync(string companyKey, string documentNumber)
        {
            var limitCompany = await _companyQuery.GetAsync(companyKey);
            var limitCustomer = await _customerQuery.GetAsync(LimitCustomer.GetId(companyKey, documentNumber));
            var limitUseds = await _limitUsedQuery.GetResumeAsync(companyKey, documentNumber);


            if (limitCustomer is null) limitCustomer = LimitCustomer.Create(companyKey, documentNumber);

            foreach (var limit in limitCompany.Limits)
                foreach (var cycle in limit.Cycles)
                    foreach (var limitLevel in cycle.LimitLevels)
                    {
                        var limitLevelCustomer = limitCustomer.GetLimitLevel(limit.Type, limit.FeatureType, cycle.Type, limitLevel.Type) ?? limitLevel;
                        var limitResume = GetResume(limitUseds, limit.Type, limit.FeatureType, cycle.Type, limitLevel.Type);

                        if(!(limitResume is null)) limitLevelCustomer.DecreaseMaxValue(limitResume.Amount);
                        limitCustomer.AddLimitLevel(limit.Type, limit.FeatureType, cycle.Type, limitLevelCustomer);
                    }


            return limitCustomer;
        }

        private LimitLevelResumeDto GetResume(IEnumerable<LimitLevelResumeDto> limitUseds,
                                              LimitType limitType,
                                              FeatureType featureType,
                                              CycleType cycleType,
                                              LevelType levelType)
            => limitUseds.FirstOrDefault(l => l.LimitType == limitType &&
                                                   l.FeatureType == featureType &&
                                                   l.CycleType == cycleType &&
                                                   l.LevelType == levelType);
    }
}
