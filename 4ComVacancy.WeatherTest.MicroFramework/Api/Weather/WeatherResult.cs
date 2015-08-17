using _4ComVacancy.WeatherTest.MicroFramework.Api.Weather;

namespace _4ComVacancy.WeatherTest.MicroFramework
{
    public class WeatherResult
    {
        public string Location { get; set; }
        public float Temperature { get; set; }
        public TemperatureUnits TemperatureUnit { get; set; }
        public float Wind { get; set; }
        public WindUnits WindUnit { get; set; }
    }
}