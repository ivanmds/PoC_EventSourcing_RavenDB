using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using PoC.ES.Api.Domain.Repositories.Limits;
using PoC.ES.Api.Infra;
using PoC.ES.Api.Infra.Repositories.Limits;
using PoC.ES.Tests.Fixtures;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PoC.ES.Tests.Infra.Repositories.Limits
{
    public class CompanyRepositoryTest
    {
        private ICompanyCommandRepository _repositoryCommand;
        private ICompanyQueryRepository _repositoryQuery;

        public CompanyRepositoryTest()
        {
            var url = "http://localhost:8080";
            _repositoryCommand = new CompanyCommandRepository(url);
            _repositoryQuery = new CompanyQueryRepository(url);
            Settings.LoadDatabase(url);
        }

        [Fact]
        public async Task AddNewCustomerSimple()
        {
            //arrange
            var random = new Random();
            var company = Fixture.GetLimitCompanyValid();
            company.AddLimit(Limit.Create(LimitType.CashIn, FeatureType.TED));

            //act
            await _repositoryCommand.SaveAsync(company);
            var companyFound = await _repositoryQuery.GetAsync(company.Id);

            //assert
            Assert.NotNull(companyFound);
            Assert.Equal(company.Id, companyFound.Id);
            Assert.True(companyFound.Limits.Count > 0);
        }
    }
}
