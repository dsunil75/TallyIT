using ParkingCalculator.Services.Parking.Rates;
using System;
using System.Collections.Generic;
using System.Text;
using ParkingCalculator.Models;
using FluentAssertions;
using Xunit;

namespace CarParkingTestProject.Services.Pricing.Rates
{
    public class WeekendRateTests
    {
        private readonly WeekendRate _rate;

        public WeekendRateTests()
        {
            _rate = new WeekendRate();
        }

        [Theory]
        [InlineData("Entry and exit Wednesday", "2020-01-01T12:00", "2020-01-01T13:00", false)]
        [InlineData("Entry Friday, exit Saturday", "2020-01-03T23:00", "2020-01-04T13:00", false)]
        [InlineData("Entry Sunday, exit Monday", "2020-01-05T12:00", "2020-01-06T13:00", false)]
        [InlineData("Valid longest midnight Friday to midnight Sunday", "2020-01-04T00:00", "2020-01-05T23:59", true)]
        [InlineData("Valid Saturday", "2020-01-04T02:00", "2020-01-04T03:00", true)]
        [InlineData("Valid Sunday", "2020-01-05T02:00", "2020-01-05T03:00", true)]
        [InlineData("Valid longest midnight Friday to midnight Sunday of next month but same weekend", "2020-02-29T00:00", "2020-03-01T23:59", true)]
        [InlineData("Invalid entry slightly too early", "2020-01-03T23:59", "2020-01-05T23:59", false)]
        [InlineData("Invalid exit slightly too late", "2020-01-04T00:00", "2020-01-06T00:00", false)]
        [InlineData("Invalid one Saturday to next Saturday", "2020-01-04T02:00", "2020-01-11T03:00", false)]
        public void TestCheckApplicableRate(string description, string entry, string exit, bool expectedIsApplicable)
        {
            var request = new CarParkingRequest
            {
                Entry = DateTime.Parse(entry),
                Exit = DateTime.Parse(exit)
            };

            var isApplicable = _rate.CheckApplicableRate(request);

            isApplicable.Should().Be(expectedIsApplicable);
        }
    }
}
