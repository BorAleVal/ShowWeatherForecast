using System;
using System.Collections.Generic;
using System.Text;

namespace ShowWeatherForecast.Models
{
    public class Wind
    {
        public int[] AvgSpeed { get; set; }
        public int[] GustSpeed { get; set; }
        public string[] Direction { get; set; }
    }
}
