using _4ComVacancy.WeatherTest.MicroFramework.Http;

namespace _4ComVacancy.WeatherTest.MicroFramework
{
    public interface IApiWeatherHttpClientBuilder
    {

        ApiWeatherHttpClient BuildApiClient(ApiConfiguration config);
    }
}