namespace Models
{
    public class WeatherData
    {
        public string City { get; set; }
        public string Condition { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public double TemperatureKelvin { get; set; }
        public double TemperatureCelsius { get; set; }
        public double TemperatureFahrenheit { get; set; }
        public double FeelsLikeKelvin { get; set; }
        public double FeelsLikeCelsius { get; set; }
        public double FeelsLikeFahrenheit { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
    }
}