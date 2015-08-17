using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using _4ComVacancy.WeatherTest.Web;
using _4ComVacancy.WeatherTest.Web.Controllers;
using NUnit.Framework;
using Microsoft.Practices.Unity;
using _4ComVacancy.WeatherTest.MicroFramework;
using _4ComVacancy.WeatherTest.Web.Infraestructure;
using _4ComVacancy.WeatherTest.MicroFramework.Api.Weather;
using Moq;
using System.Web;
using _4ComVacancy.WeatherTest.MicroFramework.Http;
using System.Web.Helpers;
using System.Web.Http.Results;
using FluentAssertions;

namespace _4ComVacancy.WeatherTest.Web.Tests.Controllers
{
    [TestFixture]
    public class WeatherControllerTest
    {
        private UnityContainer _container;        

        [SetUp]
        public void SetUp()
        {
            _container = new UnityContainer();

            _container.RegisterType<IHttpClientFactory, HttpClientFactory>();
            _container.RegisterType<IApiWeatherHttpClientBuilder, ApiWeatherHttpClientBuilder<HttpClientWrapper>>();
            _container.RegisterType<ApiWeatherManager>();

            _container.RegisterInstance<List<ApiConfiguration>>(WeatherApiConfigurationSection.ToApiConfiguration());
        }

        [Test]
        public async void JsonGetWeather_NoLocation_400()
        {
            //Arrange
            var controller = _container.Resolve<WeatherController>();

            //Act
            var result = await controller.JsonGetWeather(null, TemperatureUnits.Celsius, WindUnits.KPHs);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }


        [Test]
        public async void JsonGetWeather_Location_200()
        {
            //Arrange
            var controller = _container.Resolve<WeatherController>();

            //Act
            var result = await controller.JsonGetWeather("any location", TemperatureUnits.Celsius, WindUnits.KPHs);

            //Assert
            result.Should().BeOfType<JsonResult<WeatherResult>>();
        }


    }
}
