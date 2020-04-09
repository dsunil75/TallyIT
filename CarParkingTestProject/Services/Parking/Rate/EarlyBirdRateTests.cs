using ParkingCalculator.Services.Parking.Rates;
using System;
using System.Collections.Generic;
using System.Text;
using ParkingCalculator.Models;
using FluentAssertions;
using Xunit;

namespace CarParkingTestProject.Services.Pricing.Rates
{
    public class EarlyBirdRateTests
    {
        private readonly EarlyBirdRate _rate;

        public EarlyBirdRateTests()
        {
            _rate = new EarlyBirdRate();
        }

        [Theory]
        [InlineData("Entry too early", "2020-01-01T05:59", "2020-01-01T23:30", false)]
        [InlineData("Entry too late", "2020-01-01T09:01", "2020-01-01T23:30", false)]
        [InlineData("Exit too early", "2020-01-01T06:00", "2020-01-01T15:29", false)]
        [InlineData("Exit too late", "2020-01-01T06:00", "2020-01-01T23:31", false)]
        [InlineData("Valid longest", "2020-01-01T06:00", "2020-01-01T23:30", true)]
        [InlineData("Valid medium", "2020-01-01T07:00", "2020-01-01T22:00", true)]
        [InlineData("Valid shortest", "2020-01-01T09:00", "2020-01-01T23:30", true)]
        [InlineData("Times valid, but exit next day", "2020-01-01T07:00", "2020-01-02T22:00", false)]
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
