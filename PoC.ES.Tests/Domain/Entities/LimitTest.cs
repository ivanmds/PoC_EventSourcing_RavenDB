using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using PoC.ES.Api.Domain.Message;
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

            //act
            var result = limit.AddCycle(cycle);

            //assert
            Assert.Equal(result, MessageOfDomain.Success);
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
            var result = limit.AddCycle(cycleDouble);

            //assert
            Assert.Equal(result, MessageOfDomain.AlreadyItem);
        }
    }
}
