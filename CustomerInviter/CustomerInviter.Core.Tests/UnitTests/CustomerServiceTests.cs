using System.Collections.Generic;
using System.Linq;
using CustomerInviter.Core.Data;
using CustomerInviter.Core.Models;
using CustomerInviter.Core.Services;
using CustomerInviter.Core.Validators;
using Moq;
using NUnit.Framework;

namespace CustomerInvite.Core.Tests.UnitTests
{
    [TestFixture]
    public class CustomerServiceTests
    {
        private IGetCustomersQuery _query;
        private ICustomerService _customerService;
        private ICoordinateService _coordinateService;
        private Coordinates _sourceCoordinates;


        [SetUp]
        public void Setup()
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    Name = "Charlie Halligan",
                    Id = 28,
                    Location = new Coordinates(53.807778, -7.714444)
                },
                new Customer
                {
                    Name = "Eoin Ahearn",
                    Id = 28,
                    Location = new Coordinates(54.0894797, -6.18671)
                },
            };

            var mock = new Mock<IGetCustomersQuery>();
            mock.Setup(m => m.Execute()).Returns(customers);

            _query = mock.Object;
            _coordinateService = new CoordinateService(new CoordinateValidator());
            _customerService = new CustomerService(_coordinateService, _query);
            _sourceCoordinates = new Coordinates(53.339428, -6.257664);
        }

        /// <summary>
        /// Test getting customers within 100KM distance
        /// </summary>
        [Test]
        public void CustomersByDistance()
        {
            var customers = _customerService.GetCustomersByDistance(_sourceCoordinates, 100.00 * 1000);

            Assert.IsNotNull(customers);
            Assert.IsNotEmpty(customers);
            Assert.AreEqual(1, customers.Count());
        }

        /// <summary>
        /// Test getting customers within 100KM distance
        /// </summary>
        [Test]
        public void CustomersByDistanceInKm()
        {
            var customers = _customerService.GetCustomersByDistanceInKm(_sourceCoordinates, 100.00);

            Assert.IsNotNull(customers);
            Assert.IsNotEmpty(customers);
            Assert.AreEqual(1, customers.Count());
        }
    }
}