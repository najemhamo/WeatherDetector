using Microsoft.AspNetCore.Mvc;
using Services;

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
        private static async Task<IResult> GetWeatherByCity(string city, IWeatherService weatherService)
        {
            {
                var weatherData = await weatherService.GetWeatherAsync(city);

                if (weatherData == null)
                {
                    return TypedResults.NotFound();
                }
                else
                {
                    return TypedResults.Ok(weatherData);
                }
            }

        }


        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // private static async Task<IResult> GetCurrentTimeByCity(string city, ITimeZoneService timeZoneService, IGeocodingService geocodingService)
        // {

        //     var coordinates = await geocodingService.GetCoordinates(city);
        //     var localTime = await timeZoneService.GetLocalTime(coordinates.Latitude, coordinates.Longitude);
        //     if (coordinates == null)
        //     {
        //         return TypedResults.NotFound();
        //     }
        //     if (localTime == null)
        //     {
        //         return TypedResults.NotFound();
        //     }
        //     else
        //     {
        //         return TypedResults.Ok(localTime);
        //     }
        // }
    }
}