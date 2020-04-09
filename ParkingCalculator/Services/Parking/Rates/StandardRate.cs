using System;
using ParkingCalculator.Models;

namespace ParkingCalculator.Services.Parking.Rates
{
    public class StandardRate : IParkingRate
    {
        private const decimal HourlyRate = 5;
        private const decimal DailyRate = 20;

        public bool CheckApplicableRate(CarParkingRequest request)
        {
            return true;
        }

        public CarParkingResponse GetCarParkingDetails(CarParkingRequest request)
        {
            var hourlyCost = CalculateRate(request, x => x.TotalHours, HourlyRate);

            var dailyCost = CalculateRate(request, x => x.TotalDays, DailyRate);

            return new CarParkingResponse
            {
                Description = "Standard Rate",
                TotalPrice = Math.Min(hourlyCost, dailyCost)
            };
        }

        private decimal CalculateRate(CarParkingRequest request, Func<TimeSpan, double> selectFunc, decimal rate)
        {
            var duration = Math.Truncate(selectFunc(request.Exit - request.Entry));

            return (Convert.ToDecimal(duration) + 1) * rate;
        }
    }
}
