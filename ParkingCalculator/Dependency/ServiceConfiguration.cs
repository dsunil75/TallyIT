using ParkingCalculator.Services.Parking;
using ParkingCalculator.Services.Parking.Rates;
using Microsoft.Extensions.DependencyInjection;

namespace ParkingCalculator.Dependency
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IParkingRate, StandardRate>();
            services.AddTransient<IParkingRate, EarlyBirdRate>();
            services.AddTransient<IParkingRate, NightRate>();
            services.AddTransient<IParkingRate, WeekendRate>();
            services.AddTransient<ICarParkingService, CarParkingService>();
        }
    }
}
