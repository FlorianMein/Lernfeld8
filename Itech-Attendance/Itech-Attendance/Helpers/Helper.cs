using System.Globalization;

namespace Itech_Attendance.Helpers
{
    public static class Helper
    {
        public static TimeOnly ToTimeOnly(this string time)
        {
            TimeOnly.TryParseExact(time, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out TimeOnly parsedTime);
            return parsedTime;
        }
    }
}
