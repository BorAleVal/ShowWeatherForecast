namespace ShowWeatherForecast.Models
{
    public class Weather
    {
        public int TempretureMin { get; set; }
        public int TempretureMax { get; set; }
        public int PressureMin { get; set; }
        public int PressureMax { get; set; }
        public int Humidity { get; set; }
        public int Radiation { get; set; }
        public int Geomagnetic { get; set; }
        public string Cloudiness { get; set; }
        public Wind Wind { get; set; } = new Wind();
        public double Precipitation { get; set; }
    }
}
