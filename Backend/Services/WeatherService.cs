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
        public async Task<string> GetWeatherAsync(string city)
        {
            var apiKey = _configuration["WeatherApi:ApiKey"];
            var baseUrl = _configuration["WeatherApi:BaseUrl"];
            var url = $"{baseUrl}weather?q={city}&appid={apiKey}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

    }
}