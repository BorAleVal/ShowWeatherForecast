using ShowWeatherForecast.Models;
using System;
using System.Collections.Generic;

namespace ShowWeatherForecast.Views
{
    public interface IWeatherForecastView : IView
    {
        CityWeather CityWeather { get; set; }
        List<string> CitiesList { get; set; }
        string SelectedCity { get; }
        DateTime SelectedDate { get; }
    }
}
