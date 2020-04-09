using System;
using ParkingCalculator.Extensions;
using ParkingCalculator.Models;

namespace ParkingCalculator.Services.Parking.Rates
{
    public class WeekendRate : IParkingRate
    {
        public bool CheckApplicableRate(CarParkingRequest request)
        {
            return request.Entry.IsWeekend() &&
                   request.Exit.IsWeekend() &&
                   request.Exit - request.Entry < TimeSpan.FromDays(2);
        }

        public CarParkingResponse GetCarParkingDetails(CarParkingRequest request)
        {
            return new CarParkingResponse
            {
                Description = "Weekend Rate",
                TotalPrice = 10
            };
        }
    }
}
