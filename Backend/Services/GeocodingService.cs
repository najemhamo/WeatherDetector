using Models;
using Newtonsoft.Json.Linq;

namespace Services
{
    public class GeocodingService : IGeocodingService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GeocodingService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<GeocodingData> GetCoordinates(string city)
        {
            var apiKey = _configuration["GeocodingApi:ApiKey"];
            var baseUrl = _configuration["GeocodingApi:BaseUrl"];
            var url = $"{baseUrl}?q={city}&appid={apiKey}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var json = JArray.Parse(jsonString).FirstOrDefault();

                if (json == null)
                {
                    throw new Exception("Failed to retrieve geocoding data.");
                }

                var geocodingData = new GeocodingData
                {
                    Latitude = (double?)json["lat"] ?? 0,
                    Longitude = (double?)json["lon"] ?? 0
                };

                return geocodingData;
            }

            throw new Exception("Failed to retrieve geocoding data.");
        }
    }
}
