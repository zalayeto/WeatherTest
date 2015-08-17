using _4ComVacancy.WeatherTest.MicroFramework.Api.Weather;
using _4ComVacancy.WeatherTest.MicroFramework.Http;
using _4ComVacancy.WeatherTest.MicroFramework.Test.HttpClientResponseTest;
using _4ComVacancy.WeatherTest.Web.Infraestructure;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Moq;
using Moq.Sequences;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _4ComVacancy.WeatherTest.MicroFramework.Test
{
    [TestFixture]
    public class ApiWeatherManagerTest
    {
        private static object _lock=new object();
        private UnityContainer _container;

        [SetUp]
        public void SetUp()
        {
            _container = new UnityContainer();

            _container.RegisterType<ApiWeatherManager>();
            _container.RegisterType<ApiWeatherManager>();
            _container.RegisterType<IApiWeatherHttpClientBuilder, ApiWeatherHttpClientBuilder<HttpClientWrapper>>();
            _container.RegisterInstance<List<ApiConfiguration>>(WeatherApiConfigurationSection.ToApiConfiguration());

        }

        [Test(Description ="Two apis both returning results")]
        public async void GetWeatherAsync_2ClientsOK()
        {
            var mockHttpClient = new Mock<IHttpClient>();

            //Arrange 
            mockHttpClient.SetupSequence(x => x.GetStringAsync(It.IsAny<string>()))
            //BBC
            .Returns(Task.Run<HttpClientResponse>(() => DataTest.HttpClientResponse["BBCWeatherOK"]))
            //AccuWeather
            .Returns(Task.Run<HttpClientResponse>(() => DataTest.HttpClientResponse["AccuWeatherOK"]));
            

            var moclHttpClientFactory = new Mock<IHttpClientFactory>();
            moclHttpClientFactory.Setup(x => x.GetHttpClient<HttpClientWrapper>()).Returns(mockHttpClient.Object);

            _container.RegisterInstance<IHttpClientFactory>(moclHttpClientFactory.Object);

            var manager = _container.Resolve<ApiWeatherManager>();

            //Act
            var result = await manager.GetWeatherAsync("anylocation", Api.Weather.TemperatureUnits.Celsius, Api.Weather.WindUnits.KPHs);
            
            //Assert
            result.WindUnit.Should().Be(WindUnits.KPHs);
            result.TemperatureUnit.Should().Be(TemperatureUnits.Celsius);
            result.Wind.Should().Be(5.33f);
            result.Temperature.Should().Be(3.61f);           
        }

        [Test(Description = "Two apis one with wrong response")]
        public async void GetWeatherAsync_2ClientsWithOneClientWrong()
        {
            var mockHttpClient = new Mock<IHttpClient>();

            //Arrange 
            mockHttpClient.SetupSequence(x => x.GetStringAsync(It.IsAny<string>()))
            //BBC
            .Returns(Task.Run<HttpClientResponse>(() => DataTest.HttpClientResponse["BBCWeatherOK"]))
            //AccuWeather
            .Returns(Task.Run<HttpClientResponse>(() => DataTest.HttpClientResponse["Wrong"]));


            var moclHttpClientFactory = new Mock<IHttpClientFactory>();
            moclHttpClientFactory.Setup(x => x.GetHttpClient<HttpClientWrapper>()).Returns(mockHttpClient.Object);

            _container.RegisterInstance<IHttpClientFactory>(moclHttpClientFactory.Object);

            var manager = _container.Resolve<ApiWeatherManager>();

            //Act
            var result = await manager.GetWeatherAsync("anylocation", Api.Weather.TemperatureUnits.Farenheits, Api.Weather.WindUnits.MPHs);

            //Assert
            result.WindUnit.Should().Be(WindUnits.MPHs);
            result.TemperatureUnit.Should().Be(TemperatureUnits.Farenheits);
            result.Wind.Should().Be(0.62f);
            result.Temperature.Should().Be(77);
        }

        [Test(Description = "Two apis both with errors")]
        public async void GetWeatherAsync_2ClientsWithBothClientWrong()
        {
            var mockHttpClient = new Mock<IHttpClient>();

            //Arrange 
            mockHttpClient.SetupSequence(x => x.GetStringAsync(It.IsAny<string>()))
            //BBC
            .Returns(Task.Run<HttpClientResponse>(() => DataTest.HttpClientResponse["Wrong"]))
            //AccuWeather
            .Returns(Task.Run<HttpClientResponse>(() => DataTest.HttpClientResponse["Wrong"]));


            var moclHttpClientFactory = new Mock<IHttpClientFactory>();
            moclHttpClientFactory.Setup(x => x.GetHttpClient<HttpClientWrapper>()).Returns(mockHttpClient.Object);

            _container.RegisterInstance<IHttpClientFactory>(moclHttpClientFactory.Object);

            var manager = _container.Resolve<ApiWeatherManager>();

            //Act
            var result = await manager.GetWeatherAsync("anylocation", Api.Weather.TemperatureUnits.Celsius, Api.Weather.WindUnits.KPHs);

            //Assert
            result.WindUnit.Should().Be(WindUnits.KPHs);
            result.TemperatureUnit.Should().Be(TemperatureUnits.Celsius);
        }


    }
}
