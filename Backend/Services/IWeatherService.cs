using Models;

namespace Services
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherAsync(string city);
    }
}