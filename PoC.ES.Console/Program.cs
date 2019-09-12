using Newtonsoft.Json;
using PoC.ES.ConsoleApp.Indexs;
using PoC.ES.ConsoleApp.Model;
using Raven.Client.Documents.Session;
using System;
using System.Linq;

namespace PoC.ES.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var session = MyStore.GetDocumentStore().OpenSession())
            {
                //session.Store(new FeedEvent { AggregatedId = "1", Category = Category.Automotive, Status = Status.ACTIVATED, Name = "Buy piece", TAG = "MY_CAR", Value = 2500 });
                //session.Store(new FeedEvent { AggregatedId = "1", Status = Status.DELETED });
                //session.Store(new FeedEvent { AggregatedId = "2", Category = Category.Automotive, Status = Status.ACTIVATED, Name = "Maintenance", TAG = "MY_CAR", Value = 6005 });

                //session.Store(new FeedEvent { AggregatedId = "3", Category = Category.Ecommerce, Status = Status.ACTIVATED, Name = "Buy piece", TAG = "Online", Value = 55.10 });
                //session.Store(new FeedEvent { AggregatedId = "4", Category = Category.Ecommerce, Status = Status.ACTIVATED, Name = "Buy piece", TAG = "Online", Value = 59.99 });
                //session.Store(new FeedEvent { AggregatedId = "5", Category = Category.Ecommerce, Status = Status.ACTIVATED, Name = "Buy piece", TAG = "Online", Value = 32.33 });

                //session.Store(new FeedEvent { AggregatedId = "6", Category = Category.Food, Status = Status.ACTIVATED, Name = "Buy snack", TAG = "Online", Value = 12.10 });
                //session.Store(new FeedEvent { AggregatedId = "6", Status = Status.DELETED });

                //session.Store(new FeedEvent { AggregatedId = "7", Category = Category.Food, Status = Status.ACTIVATED, Name = "Buy snack 2", TAG = "Online", Value = 77.25 });
                //session.Store(new FeedEvent { AggregatedId = "8", Category = Category.Food, Status = Status.ACTIVATED, Name = "Buy snack 3", TAG = "Online", Value = 5.50 });
                //session.Store(new FeedEvent { AggregatedId = "9", Category = Category.Food, Status = Status.ACTIVATED, Name = "Buy snack 4", TAG = "Online", Value = 99.99 });

                //session.Store(new FeedEvent { AggregatedId = "5", Status = Status.DELETED });
                //session.SaveChanges();


                //var listFeeds = session.Query<FeedEvent, ListFeedEventAggregatedIndex>().ToList();

                //foreach (var feed in listFeeds)
                //    Console.WriteLine(JsonConvert.SerializeObject(feed) + "\n");

                Seed();
            }
        }

        public static void Seed()
        {
            //1000 => 2 min e 25 s (total de itens 25.262 no banco pq do index gravando na outra tabela)
            int aggregatedId = 10;
            for (var i = 10; i < 1000; i++)
            {
                using (var session = MyStore.GetDocumentStore().OpenSession())
                {
                    session.Store(new FeedEvent { AggregatedId = $"{aggregatedId}", Category = Category.Automotive, Status = Status.ACTIVATED, Name = "Buy piece", TAG = "MY_CAR", Value = 2500 });
                    session.Store(new FeedEvent { AggregatedId = $"{aggregatedId}", Name = "Buy piece Test" });
                    session.Store(new FeedEvent { AggregatedId = $"{aggregatedId}", Category = Category.Ecommerce });
                    session.Store(new FeedEvent { AggregatedId = $"{aggregatedId}", Status = Status.DELETED });
                    session.Store(new FeedEvent { AggregatedId = $"{aggregatedId}", TAG = "MY_CAR_2" });
                    session.Store(new FeedEvent { AggregatedId = $"{aggregatedId}", Value = 2599 });

                    session.Store(new FeedEvent { AggregatedId = $"{++aggregatedId}", Category = Category.Automotive, Status = Status.ACTIVATED, Name = "Maintenance", TAG = "MY_CAR", Value = 6005 });
                    session.Store(new FeedEvent { AggregatedId = $"{aggregatedId}", Category = Category.Food });

                    session.Store(new FeedEvent { AggregatedId = $"{++aggregatedId}", Category = Category.Ecommerce, Status = Status.ACTIVATED, Name = "Buy piece", TAG = "Online", Value = 55.10 });
                    session.Store(new FeedEvent { AggregatedId = $"{++aggregatedId}", Category = Category.Ecommerce, Status = Status.ACTIVATED, Name = "Buy piece", TAG = "Online", Value = 59.99 });
                    session.Store(new FeedEvent { AggregatedId = $"{++aggregatedId}", Category = Category.Ecommerce, Status = Status.ACTIVATED, Name = "Buy piece", TAG = "Online", Value = 32.33 });

                    session.Store(new FeedEvent { AggregatedId = $"{++aggregatedId}", Category = Category.Food, Status = Status.ACTIVATED, Name = "Buy snack", TAG = "Online", Value = 12.10 });
                    session.Store(new FeedEvent { AggregatedId = $"{aggregatedId}", Status = Status.DELETED });

                    session.Store(new FeedEvent { AggregatedId = $"{++aggregatedId}", Category = Category.Food, Status = Status.ACTIVATED, Name = "Buy snack 2", TAG = "Online", Value = 77.25 });
                    session.Store(new FeedEvent { AggregatedId = $"{++aggregatedId}", Category = Category.Food, Status = Status.ACTIVATED, Name = "Buy snack 3", TAG = "Online", Value = 5.50 });

                    session.Store(new FeedEvent { AggregatedId = $"{++aggregatedId}", Category = Category.Food, Status = Status.ACTIVATED, Name = "Buy snack 4", TAG = "Online", Value = 99.99 });
                    session.Store(new FeedEvent { AggregatedId = $"{aggregatedId}", Status = Status.DELETED });

                    Console.WriteLine(i);
                    session.SaveChanges();
                }
            }
        }
    }
}
