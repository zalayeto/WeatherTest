using _4ComVacancy.WeatherTest.MicroFramework.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4ComVacancy.WeatherTest.MicroFramework
{
    public class ApiWeatherHttpClientBuilder<T>: IApiWeatherHttpClientBuilder where T : IHttpClient, new()
    {
        private IHttpClientFactory _factory;

        public ApiWeatherHttpClientBuilder(IHttpClientFactory httpFactory)
        {
            _factory = httpFactory;
        }

        public ApiWeatherHttpClient BuildApiClient(ApiConfiguration config)
        {
            var client = _factory.GetHttpClient<T>();

            client.Timeout = config.Timeout;

            return new ApiWeatherHttpClient(client, config.Name, config.Url, config.WindKey, config.WindUnit, config.TemperatureKey, config.TemperatureUnit);
        }
    }
}