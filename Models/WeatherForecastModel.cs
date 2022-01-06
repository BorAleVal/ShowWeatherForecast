using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace ShowWeatherForecast.Models
{
    public class WeatherForecastModel : IWeatherForecastModel
    {
        private string url = "https://localhost:44356/api/WeatherCities";

        public List<string> LoadCities()
        {
            WebRequest request = WebRequest.Create(url);

            if (request == null) return null;

            WebResponse response = request.GetResponse();

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    var body = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<string>>(body);
                }
            }
        }

        public CityWeather LoadCityWeather(string CityName, DateTime date)
        {
            WebRequest request = WebRequest.Create(url + "/" + CityName);

            if (request == null) return null;

            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    var body = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<CityWeather>(body);
                }
            }
        }
    }
}
