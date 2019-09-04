using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using Xunit;

namespace PoC.ES.Tests.Domain.Entities
{
    public class LimitTest
    {
        [Fact]
        public void LimitHaveAddNewCycle()
        {
            //arrange
            var cycle = Cycle.Create(CycleType.Daily);
            var limit = Limit.Create(LimitType.CashIn, FeatureType.TED);
            limit.AddCycle(cycle);

            //act
            var result = limit.Validate();

            //assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void LimitDontHaveTwoCycleEquals()
        {
            //arrange
            var cycle = Cycle.Create(CycleType.Daily);
            var cycleDouble = Cycle.Create(CycleType.Daily);
            var limit = Limit.Create(LimitType.CashIn, FeatureType.TED);
            limit.AddCycle(cycle);

            //act
            limit.AddCycle(cycleDouble);
            var result = limit.Validate();

            //assert
            Assert.True(result.IsInvalid);
        }
    }
}
