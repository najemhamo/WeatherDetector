import { useEffect, useState } from "react";

export default function HomePage() {
  const [searchedCities, setsearchedCities] = useState([]);
  const [weatherInfo, setWeatherInfo] = useState([]);

  useEffect(() => {
    if (searchedCities.length === 0) return;

    const lastCity = searchedCities[searchedCities.length - 1];

    for (let i = 0; i < weatherInfo.length; i++) {
      if (weatherInfo[i].searchedName === lastCity) return;
    }

    fetch(`https://localhost:7021/weather/${lastCity}`)
      .then((response) => response.json())
      .then((data) => {
        data.weatherData.icon = `https://openweathermap.org/img/wn/${data.weatherData.icon}@2x.png`;
        data.weatherData.description =
          data.weatherData.description.charAt(0).toUpperCase() +
          data.weatherData.description.slice(1);
        data.weatherData.searchedName = lastCity;

        setWeatherInfo((prevData) => {
          let updatedData = [...prevData];
          if (updatedData.length === 3) {
            updatedData.shift();
          }
          return [...updatedData, data];
        });
      });
  }, [searchedCities]);

  const checkCity = (event) => {
    event.preventDefault();
    const value = event.target.cityName.value.toLowerCase();

    if (value && value.trim().length !== 0 && !searchedCities.includes(value)) {
      setsearchedCities((prevValue) => {
        let updatedCities = [...prevValue];

        if (updatedCities.length === 3) {
          updatedCities.shift();
        }
        return [...updatedCities, value];
      });
    }
  };

  const deleteCity = (index) => {
    const tmpWeatherCity = weatherInfo.filter((weather, i) => index !== i);

    setWeatherInfo([...tmpWeatherCity]);
    setsearchedCities([...tmpWeatherCity.map((city) => city.searchedName)]);
  };

  return (
    <>
      <header className={searchedCities.length === 0 ? "centeredHeader" : ""}>
        <h1 className="title">Weather Detector</h1>
        <form onSubmit={checkCity}>
          <input
            className="searchBar"
            name="cityName"
            placeholder="🔍 Search for a city"
          ></input>
          <button className="searchBtn">Search</button>
        </form>
      </header>

      {weatherInfo.length > 0 && (
        <div className="divContainer">
          {weatherInfo.map((info, index) => (
            <li key={index} className="weatherInfo">
              <button className="closeBtn" onClick={() => deleteCity(index)}>
                X
              </button>
              <img
                className="weatherIcon"
                src={info.weatherData.icon}
                alt="Weather icon"
              ></img>
              <h2 className="weatherNumber">
                {Number(info.weatherData.temperatureCelsius).toFixed(1)} °C
              </h2>
              <h3 className="weatherDesc">{info.weatherData.description}</h3>
              <h3 className="weatherText">
                Feels like:{" "}
                {Number(info.weatherData.feelsLikeCelsius).toFixed(1)}
              </h3>
              <h3 className="weatherText">
                Humidity: {info.weatherData.humidity}
              </h3>
              <h3 className="weatherText">
                Wind speed: {info.weatherData.windSpeed}
              </h3>
              {info.weatherData && (
                <p className="weatherCity">
                  {info.weatherData.city}, {info.weatherData.country}
                </p>
              )}
              {info.localTime && (
                <p className="localTime">
                  {info.localTime.formatted} {info.localTime.nextAbbreviation}
                </p>
              )}
            </li>
          ))}
        </div>
      )}
    </>
  );
}
