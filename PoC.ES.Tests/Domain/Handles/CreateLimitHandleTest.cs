using NSubstitute;
using PoC.ES.Api.Domain.Commands.Limits;
using PoC.ES.Api.Domain.Commands.Limits.Dtos;
using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using PoC.ES.Api.Domain.Handles.Limits;
using PoC.ES.Api.Domain.Repositories.Limits;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PoC.ES.Tests.Domain.Handles
{
    public class CreateLimitHandleTest
    {
        private ICompanyRepository _companyRepository;
        private CreateLimitHandle _handle;

        public CreateLimitHandleTest()
        {
            _companyRepository = Substitute.For<ICompanyRepository>();
            _handle = new CreateLimitHandle(_companyRepository);
        }

        [Fact]
        public async Task LaunchCommandCreateLimitCompanyCommandInvalidThenNotCallSaveOnRepository()
        {
            //arrange
            var command = GetCreateLimitCompanyCommandInvalid();

            //act
            var result = await _handle.Handle(command, CancellationToken.None);

            //assert
            Assert.False(result.IsValid);
            await _companyRepository.DidNotReceive().SaveAsync(Arg.Any<LimitCompany>());
        }

        [Fact]
        public async Task LaunchCommandCreateLimitCompanyCommandValidThenCallSaveOnRepository()
        {
            //arrange
            var command = GetCreateLimitCompanyCommandValid();

            //act
            var result = await _handle.Handle(command, CancellationToken.None);

            //assert
            Assert.True(result.IsValid);
            await _companyRepository.Received().SaveAsync(Arg.Any<LimitCompany>());
        }

        private CreateLimitCompanyCommand GetCreateLimitCompanyCommandInvalid()
        {
            var command = new CreateLimitCompanyCommand();
            command.CompanyKey = "ACESSO";

            return command;
        }

        private CreateLimitCompanyCommand GetCreateLimitCompanyCommandValid()
        {
            var command = new CreateLimitCompanyCommand();
            command.CompanyKey = "ACESSO";

            var limitLevels = new List<LimitLevelDto>() { new LimitLevelDto { Type = LevelType.Account, MaxValue = 1000, MinValue = 100 } };
            var cycles = new List<CycleDto>() { new CycleDto { Type = CycleType.Daily, LimitLevels = limitLevels } };
            var limits = new List<LimitDto>() { new LimitDto { Type = LimitType.CashIn, FeatureType = FeatureType.TED, Cycles = cycles } };
            command.Limits = limits;

            return command;
        }
    }
}
