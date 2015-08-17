using _4ComVacancy.WeatherTest.MicroFramework.Api.Weather;
using _4ComVacancy.WeatherTest.MicroFramework.Http;
using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace _4ComVacancy.WeatherTest.MicroFramework.Http
{
    public class ApiWeatherHttpClient: IDisposable, IWeatherApi
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ApiWeatherHttpClient));

        private readonly string _name;
        private readonly IHttpClient _httpClient ;
        private readonly Uri _baseUri;
        private readonly string _windKey;
        private readonly WindUnits _windUnit;
        private readonly string _temperatureKey;
        private readonly TemperatureUnits _temperatureUnit;

        public ApiWeatherHttpClient(IHttpClient httpClient, string name, string url, string windKey, WindUnits windUnit, string temperatureKey, TemperatureUnits tempUnit)
        {
            _httpClient = httpClient;
            _name = name;
            _baseUri = new Uri(url);
            _windKey = windKey;
            _windUnit = windUnit;
            _temperatureKey = temperatureKey;
            _temperatureUnit = tempUnit;
        }


        /// <summary>
        /// Call the url to get the weather asynchronously, once has read the json result, convert to a weather result
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public async Task<WeatherResult> GetWeather(string location)
        {
            WeatherResult weatherResult = null;

            try
            {
                var uri = new Uri(_baseUri, location);
                var response = await _httpClient.GetStringAsync(uri.ToString());

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _log.WarnFormat("{0} {1}", this._name, response.StatusCode.ToString());
                    return null;
                }                

                var jsonObj = JObject.Parse(response.Content);

                weatherResult = this.GetFromJson(jsonObj);
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("{0} {1}", this._name, ex.Message);
            }

            return weatherResult;
        }


        /// <summary>
        /// Create a weather result from a json object passed as a parameter
        /// </summary>
        /// <param name="jsonObj"></param>
        /// <returns></returns>
        private WeatherResult GetFromJson(JObject jsonObj)
        {
            WeatherResult obj = new WeatherResult();

            if (jsonObj.Count==0)
                return null;

            obj.Temperature = (float)jsonObj[this._temperatureKey];
            obj.Wind = (float)jsonObj[this._windKey];
            obj.Location = (string)jsonObj["Location"];
            obj.TemperatureUnit = this._temperatureUnit;
            obj.WindUnit = this._windUnit;

            return obj;
        }


        public void Dispose()
        {
            var disposable = _httpClient as IDisposable;

            if (disposable != null)
                disposable.Dispose();
        }


    }
}