using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using _4ComVacancy.WeatherTest.Web.Infraestructure;
using _4ComVacancy.WeatherTest.MicroFramework;
using System.Collections.Generic;
using _4ComVacancy.WeatherTest.MicroFramework.Http;
using System.Web.Http;
using Microsoft.Practices.Unity.Mvc;
using System.Web.Http.Dependencies;

namespace _4ComVacancy.WeatherTest.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IHttpClientFactory, HttpClientFactory>();
            container.RegisterType<ApiWeatherManager>();
            container.RegisterType<IApiWeatherHttpClientBuilder, ApiWeatherHttpClientBuilder<HttpClientWrapper>>();            
        }

        public static void RegisterTypesAfterStart(IUnityContainer container)
        {
            container.RegisterInstance<List<ApiConfiguration>>(WeatherApiConfigurationSection.ToApiConfiguration());
        }

        public static void RegisterComponents()
        {
            var container = GetConfiguredContainer();

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}
