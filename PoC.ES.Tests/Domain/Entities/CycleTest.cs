using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using PoC.ES.Api.Domain.Message;
using Xunit;

namespace PoC.ES.Tests.Domain.Entities
{
    public class CycleTest
    {
        [Fact]
        public void CycleHaveAddNewLimitLevel()
        {
            //arrange
            var limitLevel = LimitLevel.Create(LevelType.Account, 10000, 1000);
            var cycle = Cycle.Create(CycleType.Daily);
            
            //act
            var result = cycle.AddLimitLevel(limitLevel);

            //assert
            Assert.Equal(result, MessageOfDomain.Success);
        }

        [Fact]
        public void CycleDontHaveTwoLimitLevelEquals()
        {
            //arrange
            var limitLevel = LimitLevel.Create(LevelType.Account, 10000, 1000);
            var limitLevelDouble = LimitLevel.Create(LevelType.Account, 100012, 1000);
            var cycle = Cycle.Create(CycleType.Daily);
            cycle.AddLimitLevel(limitLevel);

            //act
            var result = cycle.AddLimitLevel(limitLevelDouble);

            //assert
            Assert.Equal(result, MessageOfDomain.AlreadyItem);
        }
    }
}
