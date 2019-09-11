using Raven.Client.Documents.Indexes;
using System.Collections.Generic;

namespace PoC.ES.ConsoleApp.Indexs
{
    public class ListFeedEventAggregatedIndex : AbstractJavaScriptIndexCreationTask
    {
        public override string IndexName => "ListFeedEventAggregatedIndex";

        public  ListFeedEventAggregatedIndex()
        {
            Maps = new HashSet<string>()
                {
                    @"map('FeedEvents', function(_event) {
                        return {
                                AggregatedId: _event.AggregatedId,
                                Category: _event.Category,
                                Status: _event.Status,
                                Name: _event.Name,
                                TAG: _event.TAG,
                                Value: _event.Value,
                                Timestamp: _event.Timestamp 
                            };
                    })"
                };
          
            AdditionalSources = new Dictionary<string, string>
            {
                ["The Script category"] = @"function getCategory(f1, f2) {
                         
                        if (f1.Category != null && f2.Category == null)
                             return f1;
                         else if (f1.Category == null && f2.Category != null)
                              return f2;

                         if (f1.Category != null && f2.Category != null && f1.Timestamp < f2.Timestamp)
                            return f2;
                         else
                             return f1;
                }",
                ["The Script status"] = @"function getStatus(f1, f2) {
                         
                        if (f1.Status != null && f2.Status == null)
                             return f1;
                         else if (f1.Status == null && f2.Status != null)
                              return f2;

                         if (f1.Status != null && f2.Status != null && f1.Timestamp < f2.Timestamp)
                            return f2;
                         else
                             return f1;
                }",
                ["The Script name"] = @"function getName(f1, f2) {
                         
                        if (f1.Name != null && f2.Name == null)
                             return f1;
                         else if (f1.Name == null && f2.Name != null)
                              return f2;

                         if (f1.Name != null && f2.Name != null && f1.Timestamp < f2.Timestamp)
                            return f2;
                         else
                             return f1;
                }",
                ["The Script tag"] = @"function getTAG(f1, f2) {
                         
                        if (f1.TAG != null && f2.TAG == null)
                             return f1;
                         else if (f1.TAG == null && f2.TAG != null)
                              return f2;

                         if (f1.TAG != null && f2.TAG != null && f1.Timestamp < f2.Timestamp)
                            return f2;
                         else
                             return f1;
                }",
                ["The Script value"] = @"function getValue(f1, f2) {
                         
                        if (f1.Value != null && f2.Value == null)
                             return f1;
                         else if (f1.Value == null && f2.Value != null)
                              return f2;

                         if (f1.Value != null && f2.Value != null && f1.Timestamp < f2.Timestamp)
                            return f2;
                         else
                             return f1;
                }",
                ["The Script timestamp"] = @"function getTimestamp(f1, f2) {
                         if (f1.Timestamp < f2.Timestamp)
                            return f2;
                         else
                             return f1;
                }"

            };

            Reduce = @"groupBy(x => x.AggregatedId)
                            .aggregate(g => {
                                return {
                                    AggregatedId: g.key,
                                    Category: g.values.reduce(getCategory).Category,
                                    Status: g.values.reduce(getStatus).Status,
                                    Name: g.values.reduce(getName).Name,
                                    TAG: g.values.reduce(getTAG).TAG,
                                    Value: g.values.reduce(getValue).Value,
                                    Timestamp: g.values.reduce(getTimestamp).Timestamp
                                };
                            })";

        }
    }
}
