using PoC.ES.Api.Domain.Entities.Limits.Types;
using System;

namespace PoC.ES.Api.Domain.Entities
{
    public class LimitUsed : AggregateRoot
    {
        public LimitUsed(string companyKey, 
                         string documentNumber, 
                         LimitType limitType, 
                         FeatureType featureType, 
                         CycleType cycleType, 
                         long amount)
        {
            CompanyKey = companyKey;
            DocumentNumber = documentNumber;
            LimitType = limitType;
            FeatureType = featureType;
            CycleType = cycleType;
            Amount = amount;
            Timestamp = DateTime.Now;
        }

        public string CompanyKey { get; private set; }
        public string DocumentNumber { get; private set; }
        public LimitType LimitType { get; private set; }
        public FeatureType FeatureType { get; private set; }
        public CycleType CycleType { get; private set; }
        public LevelType LevelType { get; private set; }
        public long Amount { get; private set; }
        public DateTime Timestamp { get; private set; }

        public static LimitUsed Create(string companyKey,
                         string documentNumber,
                         LimitType limitType,
                         FeatureType featureType,
                         CycleType cycleType,
                         long amount)
            => new LimitUsed(companyKey, documentNumber, limitType, featureType, cycleType, amount);
    }
}
