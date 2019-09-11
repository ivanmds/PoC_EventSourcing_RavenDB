using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace PoC.ES.ConsoleApp.Model
{
    public class FeedEvent
    {
        public string Id { get; set; }
        public string AggregatedId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))] public Category Category { get; set; }
        [JsonConverter(typeof(StringEnumConverter))] public Status? Status { get; set; }
        public string Name { get; set; }
        public string TAG { get; set; }
        public double? Value { get; set; }
        public DateTime Timestamp { get => DateTime.Now; }
    }
}
