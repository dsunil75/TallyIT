using ParkingCalculator.Extensions;
using ParkingCalculator.Models;

namespace ParkingCalculator.Services.Parking.Rates
{
    public class EarlyBirdRate : IParkingRate
    {
        public bool CheckApplicableRate(CarParkingRequest request)
        {
            return request.Entry.Day == request.Exit.Day &&
                   request.Entry.IsBetween(6, 9) &&
                   request.Exit.IsBetween(15.5, 23.5);
        }

        public CarParkingResponse GetCarParkingDetails(CarParkingRequest request)
        {
            return new CarParkingResponse
            {
                Description = "Early Bird",
                TotalPrice = 13
            };
        }
    }
}
