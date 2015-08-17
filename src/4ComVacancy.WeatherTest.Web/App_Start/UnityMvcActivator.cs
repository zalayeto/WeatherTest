using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(_4ComVacancy.WeatherTest.Web.App_Start.UnityWebActivator), "Start")]
[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(_4ComVacancy.WeatherTest.Web.App_Start.UnityWebActivator), "Started")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(_4ComVacancy.WeatherTest.Web.App_Start.UnityWebActivator), "Shutdown")]

namespace _4ComVacancy.WeatherTest.Web.App_Start
{
    /// <summary>Provides the bootstrapping for integrating Unity with ASP.NET MVC.</summary>
    public static class UnityWebActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start() 
        {
            var container = UnityConfig.GetConfiguredContainer();

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            // Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        public static void Started()
        {
            var container = UnityConfig.GetConfiguredContainer();

            UnityConfig.RegisterTypesAfterStart(container);
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown()
        {
            var container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }
    }
}