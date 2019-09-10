using System;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using System.Collections.Generic;
using System.Linq;
using PoC.ES.Api.Domain.Message;
using PoC.ES.Api.Domain.Validation;
using PoC.ES.Api.Results;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace PoC.ES.Api.Domain.Entities.Limits
{
    public class Cycle : IEquatable<Cycle>, IValidated
    {
        public Cycle(CycleType type)
        {
            Type = type;
        }

        [JsonConverter(typeof(StringEnumConverter))] public CycleType Type { get; private set; }

        private List<LimitLevel> _limitLevels = new List<LimitLevel>();
        public IReadOnlyCollection<LimitLevel> LimitLevels
        {
            get => _limitLevels;
            private set => _limitLevels.AddRange(value);
        }

        public void AddLimitLevels(IEnumerable<LimitLevel> limitLevels) => _limitLevels.AddRange(limitLevels);
        public void AddLimitLevel(LimitLevel limitLevel) => _limitLevels.Add(limitLevel);

        public bool Equals(Cycle other)
        {
            return Type == other.Type;
        }

        public ResultOfCommand Validate()
        {
            var result = ResultOfCommand.Create();

            foreach (var limitLevel in LimitLevels)
                if (LimitLevels.Where(p => p.Equals(limitLevel)).Count() > 1)
                    result.AddErrorMessage(MessageOfDomain.AlreadyHaveItem);

            return result;
        }


        public static Cycle Create(CycleType type) => new Cycle(type);
    }
}
