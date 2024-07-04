using Models;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Services
{
    public class TimeZoneService : ITimeZoneService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TimeZoneService> _logger;

        public TimeZoneService(HttpClient httpClient, IConfiguration configuration, ILogger<TimeZoneService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            _httpClient.BaseAddress = new Uri(_configuration["TimeZoneApi:BaseUrl"]);
        }

        public async Task<TimeZoneData> GetLocalTime(double latitude, double longitude)
        {
            // Ensure the latitude and longitude use a dot as the decimal separator
            var latitudeString = latitude.ToString(CultureInfo.InvariantCulture);
            var longitudeString = longitude.ToString(CultureInfo.InvariantCulture);

            var apiKey = _configuration["TimeZoneApi:ApiKey"];
            var url = $"?key={apiKey}&format=json&by=position&lat={latitudeString}&lng={longitudeString}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("TimeZoneDB API Response: {jsonString}", jsonString);

                var json = JObject.Parse(jsonString);

                if (json["status"]?.ToString() != "OK")
                {
                    _logger.LogError("Failed to retrieve time zone data: {message}", json["message"]?.ToString());
                    throw new Exception("Failed to retrieve time zone data.");
                }

                var timeZoneData = new TimeZoneData
                {
                    NextAbbreviation = json["nextAbbreviation"]?.ToString() ?? string.Empty,
                    Formatted = json["formatted"]?.ToString() ?? string.Empty
                };

                _logger.LogInformation("Parsed TimeZone Data: {timeZoneData}", timeZoneData);

                return timeZoneData;
            }

            var errorResponse = await response.Content.ReadAsStringAsync();
            _logger.LogError("TimeZoneDB API call failed: {errorResponse}", errorResponse);
            throw new Exception("Failed to retrieve time zone data.");
        }
    }
}
