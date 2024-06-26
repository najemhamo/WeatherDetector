import { useEffect, useState } from "react";

export default function HomePage()
{
    const [searchedCity, setSearchedCity] = useState("")
    const [weatherInfo, setWeatherInfo] = useState("")

    // GET weather information
    useEffect(() => {
        if (searchedCity === "")
            return

        fetch(`https://localhost:7021/weather/${searchedCity}`)
        .then((response) => response.json())
        .then((data) => {
            data.icon = `https://openweathermap.org/img/wn/${data.icon}@2x.png`
            setWeatherInfo(data)
        })
    }, [searchedCity])

    const checkCity = (event) =>
    {
        event.preventDefault()
        const value = event.target.cityName.value.toLowerCase()

        if (value !== undefined && value.length !== 0 && value[0] !== ' ')
            setSearchedCity(value)
    }

    return (
        <>
            <header>
                <h1>Weather Detector</h1>
                <form onSubmit={checkCity}>
                    <input className="searchBar" name="cityName" placeholder="Search for a city"></input>
                    <button className="searchBtn">Search</button>
                </form>
            </header>

            <body>
                <div className="divContainer">
                    <div>
                        <img className="weatherIcon" src={weatherInfo.icon}></img>
                        <h2 className="weatherNumber">{Number(weatherInfo.temperatureCelsius).toFixed(1)} °C</h2>
                        <p className="weatherCity">{weatherInfo.city} {weatherInfo.country}</p>
                    </div>
                </div>
            </body>
        </>
    )
}