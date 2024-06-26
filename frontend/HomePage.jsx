import { useEffect, useState } from "react";

export default function HomePage()
{
    const [searchedCity, setSearchedCity] = useState("")
    const [weatherInfo, setWeatherInfo] = useState(null)

    // GET weather information
    useEffect(() => {
        if (searchedCity === "")
            return

        fetch(`https://api.openweathermap.org/data/2.5/weather?q=${searchedCity}&appid=47cf5fe28528bd8ac7bf9585d0c70044`)
        .then((response) => response.json())
        .then((data) => {
            console.log("DATA", data)
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
                        <h2></h2>
                    </div>
                </div>
            </body>
        </>
    )
}