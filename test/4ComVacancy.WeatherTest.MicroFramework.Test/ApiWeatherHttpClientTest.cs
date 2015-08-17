using _4ComVacancy.WeatherTest.MicroFramework.Api.Weather;
using _4ComVacancy.WeatherTest.MicroFramework.Http;
using _4ComVacancy.WeatherTest.MicroFramework.Test.HttpClientResponseTest;
using _4ComVacancy.WeatherTest.Web.Infraestructure;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _4ComVacancy.WeatherTest.MicroFramework.Test
{
    [TestFixture]
    public class ApiWeatherHttpClientTest
    {
        private UnityContainer _container;

        [SetUp]
        public void SetUp()
        {
            _container = new UnityContainer();

            _container.RegisterType<ApiWeatherManager>();
            _container.RegisterType<IApiWeatherHttpClientBuilder, ApiWeatherHttpClientBuilder<HttpClientWrapper>>();

            var apis = WeatherApiConfigurationSection.ToApiConfiguration();

            _container.RegisterInstance<ApiConfiguration>("BBCWeather",apis.First(a=> a.Name.Contains("BBC")));
            _container.RegisterInstance<ApiConfiguration>("AccuWeather", apis.First(a => a.Name.Contains("Accu")));
        }

        [Test(Description ="The response received is returned correctly in a WeatherResult object for AccuWeather")]
        public async void GetWeather_AccuWeather_OK()
        {
            //Arrange 
            var mockHttpClient = new Mock<IHttpClient>();
            mockHttpClient.Setup(x => x.GetStringAsync(It.IsAny<string>())).Returns(Task.Run<HttpClientResponse>(()=>DataTest.HttpClientResponse["AccuWeatherOK"]));
                    
            var moclHttpClientFactory = new Mock<IHttpClientFactory>();
            moclHttpClientFactory.Setup(x => x.GetHttpClient<HttpClientWrapper>()).Returns(mockHttpClient.Object);

            _container.RegisterInstance<IHttpClientFactory>(moclHttpClientFactory.Object);

            var factory = _container.Resolve<ApiWeatherHttpClientBuilder<HttpClientWrapper>>();
            var client = factory.BuildApiClient(_container.Resolve<ApiConfiguration>("AccuWeather") );

            //Act
            var result = await client.GetWeather("anylocation") ;

            ////Assert
            result.Should().NotBeNull();
            result.Temperature.Should().Be(0f);
            result.Wind.Should().Be(6.0f);
            result.WindUnit.Should().Be(WindUnits.MPHs);
            result.TemperatureUnit.Should().Be(TemperatureUnits.Farenheits);
        }

        [Test(Description = "The response received is returned correctly in a WeatherResult object for BBCWeather")]
        public async void GetWeather_BBCWeather_OK()
        {
            var mockHttpClient = new Mock<IHttpClient>();

            //Arrange 
            mockHttpClient.Setup(x => x.GetStringAsync(It.IsAny<string>())).Returns(Task.Run<HttpClientResponse>(() => DataTest.HttpClientResponse["BBCWeatherOK"]));

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(x => x.GetHttpClient<HttpClientWrapper>()).Returns(mockHttpClient.Object);


            _container.RegisterInstance<IHttpClientFactory>(mockHttpClientFactory.Object);

            var factory = _container.Resolve<ApiWeatherHttpClientBuilder<HttpClientWrapper>>();
            var client = factory.BuildApiClient(_container.Resolve<ApiConfiguration>("BBCWeather"));

            //Act
            var result = await client.GetWeather("anylocation");

            //Assert
            result.Should().NotBeNull();
            result.Temperature.Should().Be((float)25.0);
            result.Wind.Should().Be((float)1.0);
            result.WindUnit.Should().Be(WindUnits.KPHs);
            result.TemperatureUnit.Should().Be(TemperatureUnits.Celsius);
        }

    }
}
