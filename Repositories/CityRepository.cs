using System.Collections.Generic;
using System.Linq;
using LiteDB;
using WeatherForecast.Entities;

namespace WeatherForecast.Repositories
{
    public class CityRepository
    {
        public static IList<City> GetCityList()
        {
            using var db = new LiteDatabase(@"Cities.db");
            var col = db.GetCollection<City>("Cities");
            return col.FindAll().ToList();
        }
    }
}
