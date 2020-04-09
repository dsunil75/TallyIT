using System;
using ParkingCalculator.Extensions;
using ParkingCalculator.Models;

namespace ParkingCalculator.Services.Parking.Rates
{
    public class NightRate : IParkingRate
    {
        public bool CheckApplicableRate(CarParkingRequest request)
        {
            return request.Entry.DayOfWeek != DayOfWeek.Saturday &&
                   request.Entry.DayOfWeek != DayOfWeek.Sunday &&
                   request.Entry.IsBetween(18, 24) &&
                   request.Exit.IsBetween(18, 6) &&
                   (
                       request.Exit.Day == request.Entry.Day ||
                       request.Exit.Day == request.Entry.Day + 1
                   );
        }

        public CarParkingResponse GetCarParkingDetails(CarParkingRequest request)
        {
            return new CarParkingResponse
            {
                Description = "Night Rate",
                TotalPrice = 6.5m
            };
        }
    }
}
