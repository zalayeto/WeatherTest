using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace _4ComVacancy.WeatherTest.MicroFramework.Http
{
    public interface IHttpClient
    {
        Task<HttpClientResponse> GetStringAsync(string uri);
        TimeSpan Timeout { get; set; }
    }
}