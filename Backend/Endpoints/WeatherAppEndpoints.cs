using Microsoft.AspNetCore.Mvc;

namespace Endpoints
{
    public static class WeatherAppEndpoints
    {
        public static void ConfigureWeatherAppEndpoints(this WebApplication app)
        {
            var weather = app.MapGroup("/weather");
            weather.MapGet("/{city}", GetWeatherByCity);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IActionResult> GetWeatherByCity(string city)
        {
            return null;
        }

    }
}