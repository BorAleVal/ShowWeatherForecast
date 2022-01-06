using System;
using System.Collections.Generic;
using System.Text;

namespace ShowWeatherForecast.Models
{
    public interface IWeatherForecastModel
    {
        List<string> LoadCities();
        CityWeather LoadCityWeather(string CityName, DateTime date);
    }
}
