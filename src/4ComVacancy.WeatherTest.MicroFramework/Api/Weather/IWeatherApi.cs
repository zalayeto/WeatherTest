using System.Threading.Tasks;

namespace _4ComVacancy.WeatherTest.MicroFramework
{
    public interface IWeatherApi
    {
        Task<WeatherResult> GetWeather(string location);
    }
}