using ShowWeatherForecast.Models;
using ShowWeatherForecast.Views;
using System;

namespace ShowWeatherForecast.Presenters
{
    public class WeatherForecastPresenter
    {
        private readonly IWeatherForecastView view;
        private readonly IWeatherForecastModel model;
        public WeatherForecastPresenter (IWeatherForecastView view, IWeatherForecastModel model)
        {
            this.view = view;
            this.model = model;
        }

        public void LoadCityWeather()
        {
            view.CityWeather = model.LoadCityWeather(view.SelectedCity, view.SelectedDate);
        }

        public void LoadCities()
        {
            view.CitiesList = model.LoadCities();
        }
    }
}
