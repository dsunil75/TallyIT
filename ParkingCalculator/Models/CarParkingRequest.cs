using System;

namespace ParkingCalculator.Models
{
    public class CarParkingRequest
    {
        public DateTime Entry { get; set; }
        public DateTime Exit { get; set; }
    }
}
