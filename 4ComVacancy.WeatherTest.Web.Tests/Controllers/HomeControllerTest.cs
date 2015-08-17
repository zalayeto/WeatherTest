using _4ComVacancy.WeatherTest.MicroFramework;
using _4ComVacancy.WeatherTest.MicroFramework.Http;
using _4ComVacancy.WeatherTest.Web.Controllers;
using _4ComVacancy.WeatherTest.Web.Infraestructure;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace _4ComVacancy.WeatherTest.Web.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
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
        public void Index()
        {
            //Arrange            
            var controller = _container.Resolve<HomeController>();

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

    }
}
