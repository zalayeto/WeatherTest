using _4ComVacancy.WeatherTest.MicroFramework;
using _4ComVacancy.WeatherTest.MicroFramework.Api.Weather;
using _4ComVacancy.WeatherTest.MicroFramework.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace _4ComVacancy.WeatherTest.MicroFramework
{
    public class ApiWeatherManager
    {
        List<IWeatherApi> _apiClients;

        public ApiWeatherManager(List<ApiConfiguration> apiConfigurations, IApiWeatherHttpClientBuilder builder)
        {
            _apiClients = new List<IWeatherApi>();

            foreach (var conf in apiConfigurations)
            {
                _apiClients.Add(builder.BuildApiClient(conf));
            }          
        }

        public async Task<WeatherResult> GetWeatherAsync(string location, TemperatureUnits tempUnit, WindUnits windUnit)
        {
            WeatherResult result=null;
            List<Task<WeatherResult>> tasks = new List<Task<WeatherResult>>();

            foreach (var api in _apiClients)
            {
                tasks.Add(api.GetWeather(location));
            }

            //Await until all the calls to GetWeather have finished.
            await Task.WhenAll(tasks);

            var interResults = tasks.Where(t=> t.Result!= null).Select(t => t.Result);

            if ( interResults.Any()) result = this.GetAverageWeatherResult(interResults,tempUnit, windUnit);

            return result;
        }

        private WeatherResult GetAverageWeatherResult(IEnumerable<WeatherResult> results, TemperatureUnits tempUnit, WindUnits windUnit)
        {
            WeatherResult aver = new WeatherResult();

            aver.TemperatureUnit = tempUnit;
            aver.WindUnit = windUnit;

            aver.Temperature = 0;
            aver.Wind = 0;

            foreach (var weatherTemp in results)
            {
                aver.Temperature += weatherTemp.TemperatureUnit != tempUnit
                    ? weatherTemp.Temperature.To(tempUnit)
                    : weatherTemp.Temperature;


                aver.Wind += weatherTemp.WindUnit != windUnit
                    ? weatherTemp.Wind.To(windUnit)
                    : weatherTemp.Wind;

            }

            aver.Temperature = (float)Math.Round( aver.Temperature / (float)results.Count(), 2);
            aver.Wind = (float)Math.Round(aver.Wind / (float)results.Count(),2);

            return aver;
        }

    }
}