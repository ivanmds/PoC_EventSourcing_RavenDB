using NSubstitute;
using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Handles.Limits;
using PoC.ES.Api.Domain.Repositories.Limits;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using PoC.ES.Tests.Fixtures;

namespace PoC.ES.Tests.Domain.Handles
{
    public class CreateLimitHandleTest
    {
        private ICompanyCommandRepository _companyRepository;
        private CreateLimitHandle _handle;

        public CreateLimitHandleTest()
        {
            _companyRepository = Substitute.For<ICompanyCommandRepository>();
            _handle = new CreateLimitHandle(_companyRepository);
        }

        [Fact]
        public async Task LaunchCommandCreateLimitCompanyCommandInvalidThenNotCallSaveOnRepository()
        {
            //arrange
            var command = Fixture.CreateLimitCompanyCommandInvalid();

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
            var command = Fixture.CreateLimitCompanyCommandValid();

            //act
            var result = await _handle.Handle(command, CancellationToken.None);

            //assert
            Assert.True(result.IsValid);
            await _companyRepository.Received().SaveAsync(Arg.Any<LimitCompany>());
        }
    }
}
