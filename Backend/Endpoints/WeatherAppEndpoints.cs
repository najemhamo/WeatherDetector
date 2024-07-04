using Microsoft.AspNetCore.Mvc;
using Services;
using Models;

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
        private static async Task<IResult> GetWeatherByCity(string city, IWeatherService weatherService, ITimeZoneService timeZoneService, IGeocodingService geocodingService)
        {
            {
                var weatherData = await weatherService.GetWeatherAsync(city);
                var coordinates = await geocodingService.GetCoordinates(city);
                var localTime = await timeZoneService.GetLocalTime(coordinates.Latitude, coordinates.Longitude);

                if (weatherData == null || coordinates == null || localTime == null)
                {
                    return TypedResults.NotFound();
                }

                else
                {
                    var response = new WeatherResponse
                    {
                        WeatherData = weatherData,
                        LocalTime = localTime
                    };

                    return TypedResults.Ok(response);
                }
            }

        }
    }
}