using System;

namespace ShowWeatherForecast.Models
{
    public class CityWeather
    {
        // TODO : непонятно, может лучше использовать общий проект, но при этом тащить лишние зависимости вместе с ним.
        public string CityName { get; set; }
        public Weather[] WeatherForecast { get; set; }
        public DateTime Date { get; set; }
    }
}
