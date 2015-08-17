using _4ComVacancy.WeatherTest.MicroFramework.Api.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4ComVacancy.WeatherTest.MicroFramework
{
    public class ApiConfiguration
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public TimeSpan Timeout { get; set; }
        public WindUnits WindUnit { get; set; }
        public TemperatureUnits TemperatureUnit { get; set; }
        public string WindKey { get; set; }
        public string TemperatureKey { get; set;}
    }
}
