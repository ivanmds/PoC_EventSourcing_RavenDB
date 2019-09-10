using PoC.ES.ConsoleApp.Model;

namespace PoC.ES.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var serssion = MyStore.GetDocumentStore().OpenSession())
            {
                serssion.Store(new FeedEvent { AggregatedId = "1", Category = Category.Automotive, Status = Status.ACTIVATED, Name = "Buy piece", TAG = "MY_CAR", Value = 2500 });
                serssion.Store(new FeedEvent { AggregatedId = "1", Status = Status.DELETED });
                serssion.Store(new FeedEvent { AggregatedId = "2", Category = Category.Automotive, Status = Status.ACTIVATED, Name = "Maintenance", TAG = "MY_CAR", Value = 6005 });

                serssion.Store(new FeedEvent { AggregatedId = "3", Category = Category.Ecommerce, Status = Status.ACTIVATED, Name = "Buy piece", TAG = "Online", Value = 55.10 });
                serssion.Store(new FeedEvent { AggregatedId = "4", Category = Category.Ecommerce, Status = Status.ACTIVATED, Name = "Buy piece", TAG = "Online", Value = 59.99 });
                serssion.Store(new FeedEvent { AggregatedId = "5", Category = Category.Ecommerce, Status = Status.ACTIVATED, Name = "Buy piece", TAG = "Online", Value = 32.33 });

                serssion.Store(new FeedEvent { AggregatedId = "6", Category = Category.Food, Status = Status.ACTIVATED, Name = "Buy snack", TAG = "Online", Value = 12.10 });
                serssion.Store(new FeedEvent { AggregatedId = "6", Status = Status.DELETED });

                serssion.Store(new FeedEvent { AggregatedId = "7", Category = Category.Food, Status = Status.ACTIVATED, Name = "Buy snack 2", TAG = "Online", Value = 77.25 });
                serssion.Store(new FeedEvent { AggregatedId = "8", Category = Category.Food, Status = Status.ACTIVATED, Name = "Buy snack 3", TAG = "Online", Value = 5.50 });
                serssion.Store(new FeedEvent { AggregatedId = "9", Category = Category.Food, Status = Status.ACTIVATED, Name = "Buy snack 4", TAG = "Online", Value = 99.99 });

                serssion.SaveChanges();

            }
        }
    }
}
