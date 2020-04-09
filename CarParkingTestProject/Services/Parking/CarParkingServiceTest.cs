using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using ParkingCalculator.Services.Parking;
using ParkingCalculator.Services.Parking.Rates;
using ParkingCalculator.Models;
using FluentAssertions;
using ParkingCalculator.Extensions;
using ParkingCalculator.Dependency;
namespace CarParkingTestProject.Services.Parking
{
    public class CarParkingServiceTest
    {
        private readonly IServiceProvider _serviceProvider;

        public CarParkingServiceTest()
        {
            var services = new ServiceCollection();

            services.ConfigureServices();
            _serviceProvider = services.BuildServiceProvider();


        }

        [Fact]
        public void GivenServicesConfigured_WhenGetServices_ThenAllServicesReceived()
        {

            _serviceProvider.GetServices<IParkingRate>().Count().Should().Be(4);
            _serviceProvider.GetService<ICarParkingService>().Should().NotBeNull();
        }

        [Theory]
        [InlineData("Wednesday 30 minutes", "2020-01-01T10:00", "2020-01-01T10:30", "Standard Rate", 5)]
        [InlineData("Wednesday 5 hours", "2020-01-01T10:00", "2020-01-01T15:00", "Standard Rate", 20)]
        [InlineData("Saturday 30 minutes", "2020-01-04T10:00", "2020-01-04T10:30", "Standard Rate", 5)]
        [InlineData("Saturday 5 hours", "2020-01-04T10:00", "2020-01-04T15:00", "Weekend Rate", 10)]
        [InlineData("Saturday early bird 10 hours", "2020-01-04T08:00", "2020-01-04T18:00", "Weekend Rate", 10)]
        [InlineData("Friday night 4 hours", "2020-01-03T18:00", "2020-01-03T22:00", "Night Rate", 6.5)]
        public void TestGetCarParkingDetails(string description, string entry, string exit, string expectedDescription, decimal expectedTotalCost)
        {
            var service = _serviceProvider.GetService<ICarParkingService>();

            var carParkingResponse = service.GetCarParkingDetails(new CarParkingRequest
            {
                Entry = DateTime.Parse(entry),
                Exit = DateTime.Parse(exit)
            });

            carParkingResponse.Description.Should().Be(expectedDescription);
            carParkingResponse.TotalPrice.Should().Be(expectedTotalCost);
        }
    }
}
