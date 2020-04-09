using ParkingCalculator.Services.Parking.Rates;
using System;
using System.Collections.Generic;
using System.Text;
using ParkingCalculator.Models;
using FluentAssertions;
using Xunit;

namespace CarParkingTestProject.Services.Pricing.Rates
{
    public class StandardRateTests
    {
        private readonly StandardRate _rate;

        public StandardRateTests()
        {
            _rate = new StandardRate();
        }

        [Theory]
        [InlineData("Minimum one hour", "2020-01-01T01:00", "2020-01-01T01:01", 5)]
        [InlineData("Maximum one hour", "2020-01-01T01:00", "2020-01-01T01:59", 5)]
        [InlineData("Minimum two hours", "2020-01-01T01:00", "2020-01-01T02:00", 10)]
        [InlineData("Maximum two hours", "2020-01-01T01:00", "2020-01-01T02:59", 10)]
        [InlineData("Minimum three hours", "2020-01-01T01:00", "2020-01-01T03:00", 15)]
        [InlineData("Maximum three hours", "2020-01-01T01:00", "2020-01-01T03:59", 15)]
        [InlineData("Minimum one day", "2020-01-01T01:00", "2020-01-01T04:00", 20)]
        [InlineData("Maximum one day", "2020-01-01T00:00", "2020-01-01T23:59", 20)]
        [InlineData("Minimum two days", "2020-01-01T00:00", "2020-01-02T00:00", 40)]
        [InlineData("Maximum two days", "2020-01-01T00:00", "2020-01-02T23:59", 40)]
        [InlineData("Four days", "2020-01-01T01:00", "2020-01-04T01:00", 80)]
        public void TestCheckApplicableRate(string description, string entry, string exit, decimal expectedTotalCost)
        {
            var request = new CarParkingRequest
            {
                Entry = DateTime.Parse(entry),
                Exit = DateTime.Parse(exit)
            };

            var pricingResponse = _rate.GetCarParkingDetails(request);

            pricingResponse.TotalPrice.Should().Be(expectedTotalCost);
        }
    }
}
