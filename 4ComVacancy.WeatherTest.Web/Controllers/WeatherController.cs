using _4ComVacancy.WeatherTest.MicroFramework;
using _4ComVacancy.WeatherTest.MicroFramework.Api.Weather;
using _4ComVacancy.WeatherTest.Web.Infraestructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace _4ComVacancy.WeatherTest.Web.Controllers
{    
    public class WeatherController : ApiController
    {
        private readonly ApiWeatherManager _manager;

        public WeatherController(ApiWeatherManager manager )
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("api/weather/{location}")]        
        public async Task<IHttpActionResult> JsonGetWeather([FromUri]string location, [FromUri]TemperatureUnits tempUnit= TemperatureUnits.Celsius, [FromUri]WindUnits windUnit = WindUnits.KPHs)
        {
            if (String.IsNullOrEmpty(location))
            {
                return BadRequest();
            }

            return Json<WeatherResult>(await _manager.GetWeatherAsync(location, tempUnit, windUnit));
        }

    }
}