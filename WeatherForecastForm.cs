using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using ShowWeatherForecast.Models;

namespace ShowWeatherForecast
{
    public partial class WeatherForecastForm : Form
    {
        private string url = "https://localhost:44356/api/WeatherCities";
        private CityWeather cityWeatherForecast;
        public WeatherForecastForm()
        {
            // TODO : по идее нужно разобраться и переделать с использованием MVP для возможности тестирования.
            InitializeComponent();
            try
            {
                WebRequest request = WebRequest.Create(url);

                if (request == null) return;

                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        var body = reader.ReadToEnd();
                        comboBoxCity.Items.AddRange(JsonConvert.DeserializeObject<List<string>>(body).ToArray());
                        comboBoxCity.SelectedIndex = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void comboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                WebRequest request = WebRequest.Create(url + "/" + comboBoxCity.SelectedItem);

                if (request == null) return;

                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        var body = reader.ReadToEnd();
                        cityWeatherForecast = JsonConvert.DeserializeObject<CityWeather>(body);
                        dateTimePicker.MinDate = cityWeatherForecast.Date;
                        dateTimePicker.Value = cityWeatherForecast.Date;
                        dateTimePicker.MaxDate = cityWeatherForecast.Date.AddDays(cityWeatherForecast.WeatherForecast.Count() - 1);
                        ShowWeatherOnDay(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ShowWeatherOnDay(int dayNum)
        {
            var weather = cityWeatherForecast.WeatherForecast[dayNum];
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
