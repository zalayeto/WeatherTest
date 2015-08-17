using _4ComVacancy.WeatherTest.MicroFramework.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _4ComVacancy.WeatherTest.MicroFramework.Test.HttpClientResponseTest
{
    public static class DataTest
    {

        public static Dictionary<string, HttpClientResponse> HttpClientResponse
        {
            get
            {
                return new Dictionary<string, HttpClientResponse> {
                    {
                        "BBCWeatherOK", new Http.HttpClientResponse {
                            Content = JsonConvert.SerializeObject(new
                            {
                                Location = "anyLocation",
                                TemperatureCelsius = 25.0,
                                WindSpeedKph = 1.0
                            }),
                            StatusCode = System.Net.HttpStatusCode.OK
                        }
                    },
                    {
                        "AccuWeatherOK", new Http.HttpClientResponse {
                            Content = JsonConvert.SerializeObject(new
                            {
                                Location = "anyLocation",
                                TemperatureFahrenheit = 0.0,
                                WindSpeedMph = 6.0
                            }),
                            StatusCode = System.Net.HttpStatusCode.OK
                        }
                    },
                    {
                        "Wrong", new Http.HttpClientResponse {
                            Content = null,
                            StatusCode = System.Net.HttpStatusCode.NotFound
                        }
                    },

                };

            }
            
        }


    }
}
