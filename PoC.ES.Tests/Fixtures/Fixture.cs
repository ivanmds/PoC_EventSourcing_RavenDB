using PoC.ES.Api.Domain.Commands.Limits;
using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using PoC.ES.Api.Domain.Limits.Dtos;
using System.Collections.Generic;

namespace PoC.ES.Tests.Fixtures
{
    public static class Fixture
    {
        public static LimitCustomer CreateLimitCustomer(LimitType limitType, FeatureType featureType, CycleType cycleType, LimitLevel limitLevel)
        {
            var customer = LimitCustomer.Create("ACESSO", "document123");

            var cycle = Cycle.Create(cycleType);
            cycle.AddLimitLevel(limitLevel);

            var limit = Limit.Create(limitType, featureType);
            limit.AddCycle(cycle);

            customer.AddLimit(limit);
            return customer;
        }

        public static CreateLimitCompanyCommand CreateLimitCompanyCommandInvalid()
        {
            var command = new CreateLimitCompanyCommand();
            command.CompanyKey = "ACESSO";

            return command;
        }

        public static CreateLimitCompanyCommand CreateLimitCompanyCommandValid()
        {
            var command = new CreateLimitCompanyCommand();
            command.CompanyKey = "ACESSO";

            var limitLevels = new List<LimitLevelDto>() { new LimitLevelDto { Type = LevelType.Account, MaxValue = 1000, MinValue = 100 } };
            var cycles = new List<CycleDto>() { new CycleDto { Type = CycleType.Daily, LimitLevels = limitLevels } };
            var limits = new List<LimitDto>() { new LimitDto { Type = LimitType.CashIn, FeatureType = FeatureType.TED, Cycles = cycles } };
            command.Limits = limits;

            return command;
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
