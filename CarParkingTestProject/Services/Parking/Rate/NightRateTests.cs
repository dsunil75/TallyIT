using ParkingCalculator.Services.Parking.Rates;
using System;
using System.Collections.Generic;
using System.Text;
using ParkingCalculator.Models;
using FluentAssertions;
using Xunit;

namespace CarParkingTestProject.Services.Pricing.Rates
{
    public class NightRateTests
    {
        private readonly NightRate _rate;

        public NightRateTests()
        {
            _rate = new NightRate();
        }

        [Theory]
        [InlineData("Entry too early", "2020-01-01T17:59", "2020-01-01T22:00", false)]
        [InlineData("Entry too late", "2020-01-01T00:00", "2020-01-01T06:00", false)]
        [InlineData("Exit too late", "2020-01-01T03:00", "2020-01-01T06:01", false)]
        [InlineData("Valid longest Wednesday to Thursday", "2020-01-01T18:00", "2020-01-02T06:00", true)]
        [InlineData("Valid longest Friday to Saturday", "2020-01-03T18:00", "2020-01-04T06:00", true)]
        [InlineData("Valid medium Wednesday to Thursday", "2020-01-01T20:00", "2020-01-02T03:00", true)]
        [InlineData("Valid shortest Wednesday", "2020-01-01T20:00", "2020-01-01T20:01", true)]
        [InlineData("Valid longest Wednesday", "2020-01-01T18:00", "2020-01-01T23:59", true)]
        [InlineData("Times valid, but stayed Wednesday to Friday", "2020-01-01T20:00", "2020-01-03T03:00", false)]
        [InlineData("Times valid, but stayed Saturday to Sunday", "2020-01-04T20:00", "2020-01-05T03:00", false)]
        [InlineData("Times valid, but stayed Sunday to Monday", "2020-01-05T20:00", "2020-01-06T03:00", false)]
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
