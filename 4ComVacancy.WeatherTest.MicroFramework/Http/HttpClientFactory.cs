using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _4ComVacancy.WeatherTest.MicroFramework.Http
{
    public interface IHttpClientFactory
    {
        IHttpClient GetHttpClient<T>() where T : IHttpClient, new ();
    }

    public class HttpClientFactory: IHttpClientFactory
    {
        public IHttpClient GetHttpClient<T>() where T :IHttpClient, new()
        {
            return new T();
        }
    }
}
