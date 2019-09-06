using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using System.Collections.Generic;
using Xunit;

namespace PoC.ES.Tests.Domain.Entities
{
    public class LimitCompanyTest
    {
        [Fact]
        public void LimitCompanyHaveAddNewLimit()
        {
            //arrange
            var limit = Limit.Create(LimitType.CashIn, FeatureType.TED);
            var limitCompany = LimitCompany.Create("123456");
            limitCompany.AddLimit(limit);

            //act
            var result = limitCompany.Validate();

            //assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void LimitCompanyDontHaveTwoLimitEquals()
        {
            //arrange
            var limit = Limit.Create(LimitType.CashIn, FeatureType.TED);
            var limitDouble = Limit.Create(LimitType.CashIn, FeatureType.TED);

            var limitCompany = LimitCompany.Create("123456");
            limitCompany.AddLimit(limit);

            //act
            limitCompany.AddLimit(limitDouble);
            var result = limitCompany.Validate();

            //assert
            Assert.True(result.IsInvalid);
        }


        public static LimitCompany GetLimitCompanyValid()
        {
            var company = LimitCompany.Create("ACESSO");
            company.AddLimits(GetLimits());
            return company;
        }

        private static IEnumerable<Limit> GetLimits()
        {
            yield return GetLimit(LimitType.CashIn, FeatureType.TED);
            yield return GetLimit(LimitType.CashIn, FeatureType.DOC);

            yield return GetLimit(LimitType.CashOut, FeatureType.TED);
            yield return GetLimit(LimitType.CashOut, FeatureType.DOC);
        }

        private static Limit GetLimit(LimitType limitType, FeatureType featureType)
        {
            var limit = Limit.Create(limitType, featureType);
            limit.AddCycles(GetCycles());
            return limit;
        }

        private static IEnumerable<Cycle> GetCycles()
        {
            yield return GetCycle(CycleType.Daily);
            yield return GetCycle(CycleType.Monthly);
            yield return GetCycle(CycleType.Transaction);
        }

        private static Cycle GetCycle(CycleType type)
        {
            var cycle = Cycle.Create(type);
            cycle.AddLimitLevel(LimitLevel.Create(LevelType.Account, 50000, 10000));
            cycle.AddLimitLevel(LimitLevel.Create(LevelType.Card, 50000, 10000));
            cycle.AddLimitLevel(LimitLevel.Create(LevelType.Document, 50000, 10000));

            return cycle;
        }
    }
}
