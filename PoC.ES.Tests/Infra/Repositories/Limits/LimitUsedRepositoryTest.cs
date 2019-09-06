using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using PoC.ES.Api.Domain.Repositories.Limits;
using PoC.ES.Api.Infra;
using PoC.ES.Api.Infra.Repositories.Limits;
using System;
using System.Linq;
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
            var limitUsed = LimitUsed.Create("ACESSO", "document123", LimitType.CashIn, FeatureType.TED, CycleType.Daily, LevelType.Account, 50);

            //act
            await _commandRepository.SaveAsync(limitUsed);
            var limitUserFound = await _queryRepository.GetAsync(limitUsed.Id);

            //assert
            Assert.NotNull(limitUserFound);
        }

        [Fact]
        public async Task GetRusumeLimitsUsed()
        {
            //arrange
            var random = new Random();
            var company = "ACESSO";
            var documentNumber = $"{random.Next(10000, 100000)}";

            long resumeAmount = 0;

            for (var i = 0; i < random.Next(5, 15); i++)
            {
                var limitUsed = LimitUsed.Create(company, documentNumber, LimitType.CashIn, FeatureType.TED, CycleType.Daily, LevelType.Account, random.Next(100, 500));
                await _commandRepository.SaveAsync(limitUsed);
                resumeAmount += limitUsed.Amount;
            }

            //act
            var resumes = await _queryRepository.GetResumeAsync(company, documentNumber);

            //assert
            Assert.NotNull(resumes);
            Assert.True(resumes.Count() == 1);
            Assert.Equal(resumeAmount, resumes.FirstOrDefault().Amount);
        }
    }
}
