namespace WeatherForecast.Contracts
{
    /// <summary>
    /// ForecastResult - result contract class for web api
    /// </summary>
    public class ForecastResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string Description { get; set; }
        public WeatherResult Weather { get; set; }
    }

    public class WeatherResult
    {
        public string Main { get; set; }
        public string Description { get; set; }
        public float Temp { get; set; }
        public float FeelsLike { get; set; }
    }
}
