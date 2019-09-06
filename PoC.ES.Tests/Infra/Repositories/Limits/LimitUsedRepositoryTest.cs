using PoC.ES.Api.Domain.Entities;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using PoC.ES.Api.Domain.Repositories.Limits;
using PoC.ES.Api.Infra;
using PoC.ES.Api.Infra.Repositories.Limits;
using System.Threading.Tasks;
using Xunit;

namespace PoC.ES.Tests.Infra.Repositories.Limits
{
    public class LimitUsedRepositoryTest
    {
        private ILimitUsedCommandRepository _commandRepository;
        private ILimitUsedQueryRepository _queryRepository;

        public LimitUsedRepositoryTest()
        {
            var url = "http://localhost:8080";
            _commandRepository = new LimitUsedCommandRepository(url);
            _queryRepository = new LimitUsedQueryRepository(url);

            Settings.LoadDatabase(url);
        }

        [Fact]
        public async Task AddLimitUsed()
        {
            //arrange
            var limitUsed = LimitUsed.Create("ACESSO", "document123", LimitType.CashIn, FeatureType.TED, CycleType.Daily, 50);

            //act
            await _commandRepository.SaveAsync(limitUsed);
            var limitUserFound = await _queryRepository.GetAsync(limitUsed.Id);

            //assert
            Assert.NotNull(limitUserFound);
        }
    }
}
