using System;

namespace ParkingCalculator.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsBetween(this DateTime self, double minimum, double maximum)
        {
            if (minimum < maximum)
            {
                return self.TimeOfDay >= TimeSpan.FromHours(minimum) &&
                       self.TimeOfDay <= TimeSpan.FromHours(maximum);
            }
            else
            {
                return self.TimeOfDay >= TimeSpan.FromHours(minimum) ||
                       self.TimeOfDay <= TimeSpan.FromHours(maximum);
            }
        }

        public static bool IsWeekend(this DateTime self)
        {
            return self.DayOfWeek == DayOfWeek.Saturday ||
                   self.DayOfWeek == DayOfWeek.Sunday;
        }

        public static bool IsWeekday(this DateTime self)
        {
            return !self.IsWeekend();
        }
    }
}
