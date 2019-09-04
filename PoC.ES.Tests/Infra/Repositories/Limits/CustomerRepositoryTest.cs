using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Repositories.Limits;
using PoC.ES.Api.Domain.Types.Limits;
using PoC.ES.Api.Infra;
using PoC.ES.Api.Infra.Repositories.Limits;
using System;
using System.Linq;
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
        public void AddNewCustomerSimple()
        {
            //arrange
            var random = new Random();
            var customer = Customer.Create($"ACESSO", $"document{random.Next(1000, 10000)}");
            customer.AddLimit(Limit.Create(LimitType.CashIn, "TED"));

            //act
            _repository.AddOrUpdate(customer);
            var customerFound = _repository.Get(customer.Id);

            //assert
            Assert.NotNull(customerFound);
            Assert.Equal(customer.Id, customerFound.Id);
            Assert.True(customerFound.Limits.Count > 0);
        }

        [Fact]
        public void AddLimitInCustomer()
        {
            //arrange
            var random = new Random();
            var customer = Customer.Create($"ACESSO", $"document{random.Next(1000, 10000)}");
            customer.AddLimit(Limit.Create(LimitType.CashIn, "TED"));
            _repository.AddOrUpdate(customer);

            //act
            customer.AddLimit(Limit.Create(LimitType.CashIn, "DOC"));
            _repository.AddOrUpdate(customer);
            var customerFound = _repository.Get(customer.Id);

            //assert
            Assert.NotNull(customerFound);
            Assert.Equal(customer.Id, customerFound.Id);
            Assert.True(customerFound.Limits.Count == 2);
        }

        [Fact]
        public void AddNewCustomerComplex()
        {
            //arrange
            var random = new Random();
            var customer = Customer.Create($"ACESSO", $"document{random.Next(1000, 10000)}");

            var limitLevel = LimitLevel.Create(LevelType.Card, 1000, 30);

            var cycle = Cycle.Create(CycleType.Transaction);
            cycle.AddLimitLevel(limitLevel);

            var limit = Limit.Create(LimitType.CashIn, "TED");
            limit.AddCycle(cycle);

            customer.AddLimit(limit);

            //act
            _repository.AddOrUpdate(customer);
            var customerFound = _repository.Get(customer.Id);

            //assert
            Assert.NotNull(customerFound);
            Assert.Equal(customer.Id, customerFound.Id);
            Assert.True(customerFound.Limits.Count == 1);
            Assert.True(customerFound.Limits.First().Cycles.Count == 1);
            Assert.True(customerFound.Limits.First().Cycles.First().LimitLevels.Count == 1);
        }

        [Fact]
        public void AddLimitInCustomerComplex()
        {
            //arrange
            var random = new Random();
            var customer = Customer.Create($"ACESSO", $"document{random.Next(1000, 10000)}");

            var limitLevel = LimitLevel.Create(LevelType.Card, 1000, 30);
            var cycle = Cycle.Create(CycleType.Transaction);
            cycle.AddLimitLevel(limitLevel);
            var limit = Limit.Create(LimitType.CashIn, "TED");
            limit.AddCycle(cycle);

            var limitLevel2 = LimitLevel.Create(LevelType.Document, 1000, 30);
            var cycle2 = Cycle.Create(CycleType.Transaction);
            cycle2.AddLimitLevel(limitLevel2);
            var limit2 = Limit.Create(LimitType.CashIn, "DOC");
            limit2.AddCycle(cycle2);

            customer.AddLimit(limit);
            _repository.AddOrUpdate(customer);

            //act
            customer.AddLimit(limit2);
            _repository.AddOrUpdate(customer);

            var customerFound = _repository.Get(customer.Id);

            //assert
            Assert.NotNull(customerFound);
            Assert.Equal(customer.Id, customerFound.Id);
            Assert.True(customerFound.Limits.Count == 2);
        }
    }
}
