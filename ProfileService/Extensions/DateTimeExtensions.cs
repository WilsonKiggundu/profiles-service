using System;

namespace ProfileService.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsToday(this DateTime dateTime)
        {
            return dateTime.Date == DateTime.Today;
        }

        public static bool IsMoreThanDaysAway(this DateTime dateTime, int days)
        {
            return dateTime.AddDays(-1 * days).Date >= DateTime.Today;
        }
    }
}