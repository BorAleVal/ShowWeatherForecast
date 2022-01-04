using System;

namespace ShowWeatherForecast.Models
{
    public class CityWeather
    {
        // TODO : Вероятно что-то нужно вынести в отдельный проект, что бы использовать во всех решениях.
        public string CityName { get; set; }
        public Weather Weather { get; set; } = new Weather();
        public DateTime Date { get; set; }
    }
}
