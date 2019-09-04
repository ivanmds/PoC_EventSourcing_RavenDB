using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Entities.Limits.Types;
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
            cycle.AddLimitLevel(limitLevel);

            //act
            var result = cycle.Validate();

            //assert
            Assert.True(result.IsValid);
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
            cycle.AddLimitLevel(limitLevelDouble);
            var result = cycle.Validate();

            //assert
            Assert.True(result.IsInvalid);
        }
    }
}
