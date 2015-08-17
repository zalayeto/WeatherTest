using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4ComVacancy.WeatherTest.MicroFramework.Http
{
    public class HttpClientResponse
    {
        public string ContentType { get; set;}
        public string Content { get; set; }
        public System.Net.HttpStatusCode StatusCode { get; set; }
        public string StatusCodeReason { get; set; }

    }
}
