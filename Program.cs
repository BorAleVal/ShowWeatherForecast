using ShowWeatherForecast.Views;
using System;
using System.Windows.Forms;

namespace ShowWeatherForecast
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var view = new WeatherForecastForm();
            Application.Run(view);
        }
    }
}
