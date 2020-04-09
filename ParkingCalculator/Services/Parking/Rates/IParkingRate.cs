using ParkingCalculator.Models;

namespace ParkingCalculator.Services.Parking.Rates
{
    public interface IParkingRate
    {
        bool CheckApplicableRate(CarParkingRequest request);
        CarParkingResponse GetCarParkingDetails(CarParkingRequest request);
    }
}
