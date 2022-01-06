using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShowWeatherForecast.Models;
using ShowWeatherForecast.Presenters;
using NLog;

namespace ShowWeatherForecast.Views
{
    public partial class WeatherForecastForm : Form , IWeatherForecastView
    {
        private WeatherForecastPresenter presenter;

        public CityWeather CityWeather { get; set; }
        public List<string> CitiesList { get; set; }
        public string SelectedCity { get => comboBoxCity.SelectedItem.ToString(); }
        public DateTime SelectedDate { get => dateTimePicker.Value; }

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public WeatherForecastForm()
        {
            var model = new WeatherForecastModel();
            presenter = new WeatherForecastPresenter(this, model);

            InitializeComponent();

            try
            {
                presenter.LoadCities();
                comboBoxCity.Items.AddRange(CitiesList.ToArray());
                comboBoxCity.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                MessageBox.Show("Не удалось получить данные о городах.");
            }
        }

        private void comboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                presenter.LoadCityWeather();
                dateTimePicker.MinDate = CityWeather.Date;
                dateTimePicker.Value = CityWeather.Date;
                dateTimePicker.MaxDate = CityWeather.Date.AddDays(CityWeather.WeatherForecast.Count() - 1);
                ShowWeatherOnDay(0);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                MessageBox.Show($"Не удалось получить данные о погоде в городе {CityWeather.CityName}.");
            }
        }

        private void ShowWeatherOnDay(int dayNum)
        {
            if (CityWeather == null) return;

            if (CityWeather.WeatherForecast.Length < dayNum)
            {
                MessageBox.Show($"Не удалось получить данные о погоде в городе {CityWeather.CityName}.");
                return;
            }

            var weather = CityWeather.WeatherForecast[dayNum];
            richTextBox.Text = $"Температура {weather.TempretureMin}..{weather.TempretureMax}\n{weather.Cloudiness}" +
                $"\nВетер {weather.Wind.Direction} {weather.Wind.AvgSpeed}м/с с порывами до {weather.Wind.GustSpeed}м/с" +
                $"\nДавление {weather.PressureMin}-{weather.PressureMax}мм рт. ст." +
                $"\nВлажность {weather.Humidity}%" +
                $"\nОсадки {weather.Precipitation}мм" +
                $"\nГеомагнитная активность {weather.Geomagnetic}Кп-индекс" +
                $"\nУльтрафиолетовый индекс {weather.Radiation}";
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan difference = dateTimePicker.Value - dateTimePicker.MinDate;
            ShowWeatherOnDay(difference.Days);
        }
    }
}
