using PoC.ES.Api.Domain.Entities;
using PoC.ES.Api.Domain.Repositories;
using PoC.ES.Api.Infra;
using PoC.ES.Api.Infra.Repositories;
using Xunit;

namespace PoC.ES.Tests.Infra.Repositories
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
        public void AddNewCustomer()
        {
            ////arrange
            //var customer = new LimitConfiguration("company123", "document123");
            //customer.AddLimit(new Limit("documento", 500));

            ////act
            //_repository.Add(customer);
            //var customerFound = _repository.Get(customer.Id);

            ////assert
            //Assert.NotNull(customerFound);
            //Assert.Equal(customer.Id, customerFound.Id);
            //Assert.True(customerFound.Limits.Count > 0);
        }

        [Fact]
        public void AddLimitInCustomer()
        {
            ////arrange
            //var customer = new LimitConfiguration("company123", "document123");
            //customer.AddLimit(new Limit("documento", 500));
            //_repository.Add(customer);

            ////act
            //_repository.AddLimit(customer.Id, new Limit("cartão", 300));
            //var customerFound = _repository.Get(customer.Id);

            ////assert
            //Assert.NotNull(customerFound);
            //Assert.Equal(customer.Id, customerFound.Id);
            //Assert.True(customerFound.Limits.Count == 2);
        }
    }
}
