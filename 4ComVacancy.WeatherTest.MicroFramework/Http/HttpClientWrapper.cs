using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace _4ComVacancy.WeatherTest.MicroFramework.Http
{
    public class HttpClientWrapper : IHttpClient, IDisposable
    {
        HttpClient _httpClient = new HttpClient();

        public TimeSpan Timeout
        {
            get
            {
                return _httpClient.Timeout;
            }

            set
            {
                _httpClient.Timeout = value;
            }
        }

        public HttpClientWrapper()
        {
        }

        public async Task<HttpClientResponse> GetStringAsync(string uri)
        {
            var httpRes = await _httpClient.GetAsync(uri);

            var response = new HttpClientResponse();

            response.StatusCode = httpRes.StatusCode;
            response.Content = await httpRes.Content.ReadAsStringAsync();
            response.StatusCodeReason = httpRes.ReasonPhrase;
            response.ContentType = httpRes.Content.Headers.ContentType.MediaType;        

            return response;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }


    }
}