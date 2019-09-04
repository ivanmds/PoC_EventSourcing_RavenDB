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
    public class CustomerRepositoryTest
    {
        private ICustomerRepository _repository;

        public CustomerRepositoryTest()
        {
            var url = "http://localhost:8080";
            _repository = new CustomerRepository(url);
            Settings.LoadDatabase(url);
        }

        [Fact]
        public async Task AddNewCustomerSimple()
        {
            //arrange
            var random = new Random();
            var customer = LimitCustomer.Create($"ACESSO", $"document{random.Next(1000, 10000)}");
            customer.AddLimit(Limit.Create(LimitType.CashIn, FeatureType.TED));

            //act
            await _repository.AddOrUpdateAsync(customer);
            var customerFound = await _repository.GetAsync(customer.Id);

            //assert
            Assert.NotNull(customerFound);
            Assert.Equal(customer.Id, customerFound.Id);
            Assert.True(customerFound.Limits.Count > 0);
        }

        [Fact]
        public async Task AddLimitInCustomer()
        {
            //arrange
            var random = new Random();
            var customer = LimitCustomer.Create($"ACESSO", $"document{random.Next(1000, 10000)}");
            customer.AddLimit(Limit.Create(LimitType.CashIn, FeatureType.TED));
            await _repository.AddOrUpdateAsync(customer);

            //act
            customer.AddLimit(Limit.Create(LimitType.CashIn, FeatureType.DOC));
            await _repository.AddOrUpdateAsync(customer);
            var customerFound = await _repository.GetAsync(customer.Id);

            //assert
            Assert.NotNull(customerFound);
            Assert.Equal(customer.Id, customerFound.Id);
            Assert.True(customerFound.Limits.Count == 2);
        }

        [Fact]
        public async Task AddNewCustomerComplex()
        {
            //arrange
            var random = new Random();
            var customer = LimitCustomer.Create($"ACESSO", $"document{random.Next(1000, 10000)}");

            var limitLevel = LimitLevel.Create(LevelType.Card, 1000, 30);

            var cycle = Cycle.Create(CycleType.Transaction);
            cycle.AddLimitLevel(limitLevel);

            var limit = Limit.Create(LimitType.CashIn, FeatureType.TED);
            limit.AddCycle(cycle);

            customer.AddLimit(limit);

            //act
            await _repository.AddOrUpdateAsync(customer);
            var customerFound = await _repository.GetAsync(customer.Id);

            //assert
            Assert.NotNull(customerFound);
            Assert.Equal(customer.Id, customerFound.Id);
            Assert.True(customerFound.Limits.Count == 1);
            Assert.True(customerFound.Limits.First().Cycles.Count == 1);
            Assert.True(customerFound.Limits.First().Cycles.First().LimitLevels.Count == 1);
        }

        [Fact]
        public async Task AddLimitInCustomerComplex()
        {
            //arrange
            var random = new Random();
            var customer = LimitCustomer.Create($"ACESSO", $"document{random.Next(1000, 10000)}");

            var limitLevel = LimitLevel.Create(LevelType.Card, 1000, 30);
            var cycle = Cycle.Create(CycleType.Transaction);
            cycle.AddLimitLevel(limitLevel);
            var limit = Limit.Create(LimitType.CashIn, FeatureType.TED);
            limit.AddCycle(cycle);

            var limitLevel2 = LimitLevel.Create(LevelType.Document, 1000, 30);
            var cycle2 = Cycle.Create(CycleType.Transaction);
            cycle2.AddLimitLevel(limitLevel2);
            var limit2 = Limit.Create(LimitType.CashIn, FeatureType.DOC);
            limit2.AddCycle(cycle2);

            customer.AddLimit(limit);
            await _repository.AddOrUpdateAsync(customer);

            //act
            customer.AddLimit(limit2);
            await _repository.AddOrUpdateAsync(customer);

            var customerFound = await _repository.GetAsync(customer.Id);

            //assert
            Assert.NotNull(customerFound);
            Assert.Equal(customer.Id, customerFound.Id);
            Assert.True(customerFound.Limits.Count == 2);
        }
    }
}
