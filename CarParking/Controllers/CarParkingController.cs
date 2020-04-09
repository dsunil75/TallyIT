using System;
using Microsoft.AspNetCore.Mvc;
using ParkingCalculator.Services.Parking.Rates;
using ParkingCalculator.Services.Parking;
using ParkingCalculator.Models;

namespace Web.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CarParkingController : ControllerBase
    {
        private readonly ICarParkingService _service;

        public CarParkingController(ICarParkingService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetCarParkingDetails(DateTime startDate, DateTime endDate)
        {
            try
            {
                CarParkingRequest request = new CarParkingRequest
                {
                    Entry = startDate,
                    Exit = endDate

                };

                var carParkingResponse = _service.GetCarParkingDetails(request);
                return Ok(carParkingResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }
    }
}