using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using PoC.ES.Api.Domain.Repositories.Limits;
using PoC.ES.Api.Infra;
using PoC.ES.Api.Infra.Repositories.Limits;
using System;
using Xunit;

namespace PoC.ES.Tests.Infra.Repositories.Limits
{
    public class CompanyRepositoryTest
    {
        private ICompanyRepository _repository;

        public CompanyRepositoryTest()
        {
            var url = "http://localhost:8080";
            _repository = new CompanyRepository(url);
            Settings.LoadDatabase(url);
        }

        [Fact]
        public void AddNewCustomerSimple()
        {
            //arrange
            var random = new Random();
            var company = LimitCompany.Create($"ACESSO{random.Next(1000, 10000)}");
            company.AddLimit(Limit.Create(LimitType.CashIn, FeatureType.TED));

            //act
            _repository.AddOrUpdate(company);
            var companyFound = _repository.Get(company.Id);

            //assert
            Assert.NotNull(companyFound);
            Assert.Equal(company.Id, companyFound.Id);
            Assert.True(companyFound.Limits.Count > 0);
        }
    }
}
