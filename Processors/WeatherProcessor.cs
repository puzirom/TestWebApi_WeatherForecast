using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using WeatherForecast.Contracts;
using WeatherForecast.Entities;
using WeatherForecast.Repositories;

namespace WeatherForecast.Processors
{
    /// <summary>
    /// Class includes all logic to handle a Weather Forecast
    /// </summary>
    public class WeatherProcessor
    {
        private const string WebApiPath = @"http://api.openweathermap.org/data/2.5/group?id={0}&APPID=43e305856ff7bf71c05b88353b942565&units=metric";
        private static readonly HttpClient Client = new HttpClient();

        public static async Task<IEnumerable<ForecastResult>> GetForecast()
        {
            var cities = CityRepository.GetCityList();
            var response = await GetForecastAsync(cities);
            var result = GetForecastResult(cities, response);
            return result;
        }

        private static async Task<ForecastResponse> GetForecastAsync(IList<City> cities)
        {
            if (cities == null)
            {
                throw new ArgumentNullException(nameof(cities));
            }

            var ids = cities.Select(x => x.Id).ToArray();
            var parameters = string.Join(",", ids);
            var requestUri = string.Format(WebApiPath, parameters);
            var streamTask = await Client.GetStreamAsync(requestUri);
            var response = await JsonSerializer.DeserializeAsync<ForecastResponse>(streamTask);
            return response;
        }

        private static IEnumerable<ForecastResult> GetForecastResult(IList<City> cities, ForecastResponse response)
        {
            var result =
                from item in response.list
                let weather = new WeatherResult
                {
                    Main = item.weather[0].main,
                    Description = item.weather[0].description,
                    FeelsLike = item.main.feels_like,
                    Temp = item.main.temp
                }
                let city = cities.Single(c => c.Id == item.id)
                select new ForecastResult
                {
                    Id = item.id,
                    Name = city.Name,
                    CountryCode = city.CountryCode,
                    Description = city.Description,
                    Weather = weather
                };
            return result.ToList();
        }
    }
}
