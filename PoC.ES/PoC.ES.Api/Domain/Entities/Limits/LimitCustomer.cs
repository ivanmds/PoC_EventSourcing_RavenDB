using PoC.ES.Api.Domain.Entities.Limits.Types;
using System.Linq;

namespace PoC.ES.Api.Domain.Entities.Limits
{
    public class LimitCustomer : LimitConfiguration
    {
        public LimitCustomer(string companyKey, string documentNumber, bool registrationCompleted = false)
            :base(companyKey)
        {
            DocumentNumber = documentNumber;
            RegistrationCompleted = registrationCompleted;
            Id = GetId(CompanyKey, DocumentNumber);
        }

        public string DocumentNumber { get; private set; }
        public bool RegistrationCompleted { get; private set; }

        public LimitLevel GetLimitLevel(LimitType limitType, FeatureType featureType, CycleType cycleType, LevelType levelType)
        {
            var limit = Limits.FirstOrDefault(l => l.Type == limitType && l.FeatureType == featureType);
            if (limit is null) return null;

            var cycle = limit.Cycles.FirstOrDefault(c => c.Type == cycleType);
            if (cycle is null) return null;

            return cycle.LimitLevels.FirstOrDefault(l => l.Type == levelType);
        }


        public bool HasLimitLevel(LimitType limitType, FeatureType featureType, CycleType cycleType, LevelType levelType)
            => !(GetLimitLevel(limitType, featureType, cycleType, levelType) is null);

        public bool NotHasLimitLevel(LimitType limitType, FeatureType featureType, CycleType cycleType, LevelType levelType)
            => !HasLimitLevel(limitType, featureType, cycleType, levelType);

        public void AddLimitLevel(LimitType limitType, FeatureType featureType, CycleType cycleType, LimitLevel limitLevel)
        {
            var limit = Limits.FirstOrDefault(l => l.Type == limitType && l.FeatureType == featureType);
            if (limit is null)
            {
                limit = Limit.Create(limitType, featureType);
                AddLimit(limit);
            }

            var cycle = limit.Cycles.FirstOrDefault(c => c.Type == cycleType);
            if (cycle is null)
            {
                cycle = Cycle.Create(cycleType);
                limit.AddCycle(cycle);
            }

            cycle.AddLimitLevel(limitLevel);
        }



        public static LimitCustomer Create(string companyKey, string documentNumber, bool registrationCompleted = false)
          => new LimitCustomer(companyKey, documentNumber, registrationCompleted);

        public static string GetId(string companyKey, string documentNumber) => $"{companyKey}#{documentNumber}";
    }
}
