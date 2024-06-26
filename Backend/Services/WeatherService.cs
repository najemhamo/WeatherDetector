using Models;
using Newtonsoft.Json.Linq;

namespace Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<WeatherData> GetWeatherAsync(string city)
        {
            var apiKey = _configuration["WeatherApi:ApiKey"];
            var baseUrl = _configuration["WeatherApi:BaseUrl"];
            var url = $"{baseUrl}weather?q={city}&appid={apiKey}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(jsonString);

                var temperatureKelvin = (double?)json["main"]?["temp"] ?? 0;
                var feelsLikeKelvin = (double?)json["main"]?["feels_like"] ?? 0;

                var weatherData = new WeatherData
                {
                    City = json["name"]?.ToString(),
                    Condition = json["weather"]?[0]?["main"]?.ToString(),
                    Country = json["sys"]?["country"]?.ToString(),
                    Description = json["weather"]?[0]?["description"]?.ToString(),
                    Icon = json["weather"]?[0]?["icon"]?.ToString(),
                    TemperatureKelvin = temperatureKelvin,
                    TemperatureCelsius = temperatureKelvin - 273.15,
                    TemperatureFahrenheit = (temperatureKelvin - 273.15) * 9 / 5 + 32,
                    FeelsLikeKelvin = feelsLikeKelvin,
                    FeelsLikeCelsius = feelsLikeKelvin - 273.15,
                    FeelsLikeFahrenheit = (feelsLikeKelvin - 273.15) * 9 / 5 + 32,
                    Humidity = (int?)json["main"]?["humidity"] ?? 0,
                    WindSpeed = (double?)json["wind"]?["speed"] ?? 0,
                };

                return weatherData;
            }

            return null;
        }
    }
}
