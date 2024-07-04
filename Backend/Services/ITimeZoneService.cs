using Models;

namespace Services
{
    public interface ITimeZoneService
    {
        public Task<TimeZoneData> GetLocalTime(double latitude, double longitude);
    }
}