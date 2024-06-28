import { useEffect, useState } from "react";

export default function HomePage()
{
    const [searchedCities, setsearchedCities] = useState([]);
    const [weatherInfo, setWeatherInfo] = useState([]);

    // GET weather information
    useEffect(() => {
        if (searchedCities.length === 0)
            return;

        const lastCity = searchedCities[searchedCities.length - 1];

        fetch(`https://localhost:7021/weather/${lastCity}`)
        .then((response) => response.json())
        .then((data) => {
            data.icon = `https://openweathermap.org/img/wn/${data.icon}@2x.png`
            data.description =  data.description.charAt(0).toUpperCase() +  data.description.slice(1)
            console.log("DATA", data)

            setWeatherInfo((prevData) => {
                let updatedData = [...prevData];
                if (updatedData.length === 3) {
                    updatedData.shift();
                }
                return [...updatedData, data];
            });
        });
    }, [searchedCities]);

    const checkCity = (event) =>
    {
        event.preventDefault()
        const value = event.target.cityName.value.toLowerCase()

        if (
            value && 
            value.trim().length !== 0 && 
            !searchedCities.includes(value)
            )

            {
            setsearchedCities((prevValue) => 
                {
                let updatedCities = [...prevValue];
                
                if (updatedCities.length === 3) 
                    {updatedCities.shift();}
                return [...updatedCities, value];
                });
            }
    };

    return (
        <>
            <header>
                <h1>Weather Detector</h1>
                <form onSubmit={checkCity}>
                    <input 
                    className="searchBar" 
                    name="cityName" 
                    placeholder="Search for a city">
                    </input>
                    <button className="searchBtn">
                        Search
                    </button>
                </form>
            </header>

            <body>
                <div className="divContainer">
                    {weatherInfo.map((info, index) => (
                        <li key={index} className="weatherInfo">
                        <img className="weatherIcon" src={info.icon}>
                        </img>
                        <h2 className="weatherNumber">
                            {Number(info.temperatureCelsius).toFixed(1)} °C
                        </h2>
                        <h3 className="weatherText">
                            Feels like: {Number(info.feelsLikeCelsius).toFixed(1)}
                        </h3>
                        <h3 className="weatherText">
                            Humidity: {info.humidity}
                        </h3>
                        <h3 className="weatherText">
                            Wind speed: {info.windSpeed}
                        </h3>
                        <h3 className="weatherDesc">
                            {info.description}
                        </h3>
                        {
                        info && 
                        <p className="weatherCity">
                            {info.city}, {info.country}
                        </p>}
                        </li>
                    ))}
                </div>
            </body>
        </>
    )
}