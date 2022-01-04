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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ShowWeatherForecast.Models;
using System.Reflection;

namespace ShowWeatherForecast
{
    public partial class Form1 : Form
    {
        private string url = "https://localhost:44356/api/WeatherCities";
        private Weather weather;
        public Form1()
        {
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
                //MessageBox.Show(barnWeather.CityName + "\n" + string.Join(",", barnWeather.Weather.Tempreture_max));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                        var weatherForecast = JsonConvert.DeserializeObject<CityWeather>(body);
                        weather = weatherForecast.Weather;
                        dateTimePicker.MinDate = weatherForecast.Date;
                        dateTimePicker.Value = weatherForecast.Date;
                        dateTimePicker.MaxDate = weatherForecast.Date.AddDays(weatherForecast.Weather.TempretureMax.Count() - 1);
                        //ShowWeatherOnDay(0);
                        //MessageBox.Show(weatherForecast.CityName + "\n" + string.Join(",", weatherForecast.Weather.Tempreture_max));
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
            richTextBox.Text = $"Температура {weather.TempretureMin[dayNum]}..{weather.TempretureMax[dayNum]}\n{weather.Cloudiness[dayNum]}" +
                $"\nВетер {weather.Wind.Direction[dayNum]} {weather.Wind.AvgSpeed[dayNum]}м/с с порывами до {weather.Wind.GustSpeed[dayNum]}м/с" +
                $"\nДавление {weather.PressureMin[dayNum]}-{weather.PressureMax[dayNum]}мм рт. ст." +
                $"\nВлажность {weather.Humidity[dayNum]}%" +
                $"\nОсадки {weather.Precipitation[dayNum]}мм" +
                $"\nГеомагнитная активность {weather.Geomagnetic[dayNum]}Кп-индекс" +
                $"\nУльтрафиолетовый индекс {weather.Radiation[dayNum]}";
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan difference = dateTimePicker.Value - dateTimePicker.MinDate;
            ShowWeatherOnDay(difference.Days);
        }
    }
}
