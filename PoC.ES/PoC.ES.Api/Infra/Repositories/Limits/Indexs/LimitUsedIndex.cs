using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using Raven.Client.Documents.Indexes;
using System.Linq;

namespace PoC.ES.Api.Infra.Repositories.Limits.Indexs
{
    public class LimitUsedIndex : AbstractIndexCreationTask<LimitUsed, LimitUsedIndex.Reuslt>
    {
        public class Reuslt
        {
            public string CompanyKey { get; set; }
            public string DocumentNumber { get; set; }
            public LimitType LimitType { get; set; }
            public FeatureType FeatureType { get; set; }
            public CycleType CycleType { get; set; }
            public LevelType LevelType { get; set; }
            public long Amount { get; set; }
        }

        public LimitUsedIndex()
        {
            Map = limitUseds => from limit in limitUseds
                                select new
                                {
                                    limit.CompanyKey,
                                    limit.DocumentNumber,
                                    limit.LimitType,
                                    limit.FeatureType,
                                    limit.CycleType,
                                    limit.LevelType,
                                    limit.Amount
                                };

            Reduce = results => from result in results
                                group result by new
                                {
                                    result.CompanyKey,
                                    result.DocumentNumber,
                                    result.LimitType,
                                    result.FeatureType,
                                    result.CycleType,
                                    result.LevelType
                                } into g
                                select new
                                {
                                    g.Key.CompanyKey,
                                    g.Key.DocumentNumber,
                                    g.Key.LimitType,
                                    g.Key.FeatureType,
                                    g.Key.CycleType,
                                    g.Key.LevelType,
                                    Amount = g.Sum(p => p.Amount)
                                };

        }
    }
}
