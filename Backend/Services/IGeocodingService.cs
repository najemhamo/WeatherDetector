using Models;

namespace Services
{
    public interface IGeocodingService
    {
        public Task<GeocodingData> GetCoordinates(string city);
    }
}