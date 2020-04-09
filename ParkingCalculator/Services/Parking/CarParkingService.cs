using System;
using System.Collections.Generic;
using System.Linq;
using ParkingCalculator.Models;
using ParkingCalculator.Services.Parking.Rates;
namespace ParkingCalculator.Services.Parking
{
    public interface ICarParkingService
    {
        CarParkingResponse GetCarParkingDetails(CarParkingRequest request);
    }

    public class CarParkingService : ICarParkingService
    {
        private readonly IEnumerable<IParkingRate> _rates;

        public CarParkingService(IEnumerable<IParkingRate> rates)
        {
            _rates = rates;
        }

        public CarParkingResponse GetCarParkingDetails(CarParkingRequest request)
        {
            if (request.Exit < request.Entry)
            {
                throw new Exception("Entry must be earlier than Exit");
            }

            return _rates
                .Where(x => x.CheckApplicableRate(request))
                .Select(x => x.GetCarParkingDetails(request))
                .OrderBy(x => x.TotalPrice)
                .First();
        }
    }
}
