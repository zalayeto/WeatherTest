using _4ComVacancy.WeatherTest.MicroFramework;
using _4ComVacancy.WeatherTest.MicroFramework.Api.Weather;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace _4ComVacancy.WeatherTest.Web.Infraestructure
{
    public class WeatherApiConfigurationSection : ConfigurationSection
    {

        private static WeatherApiConfigurationSection _settings
          = ConfigurationManager.GetSection("weatherApi") as WeatherApiConfigurationSection;

        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public WebApisInstanceCollection WebApis
        {
            get { return (WebApisInstanceCollection)this[""]; }
            set { this[""] = value; }
        }

        public static List<ApiConfiguration> ToApiConfiguration()
        {
            var res = new List<ApiConfiguration>();

            var enumApis = _settings.WebApis.GetEnumerator();

            while (enumApis.MoveNext())
            {
                var api = enumApis.Current as WebApiInstanceElement;
                res.Add(new ApiConfiguration {
                    Name = api.Name,
                    Timeout = new TimeSpan( 0,0,0,0,api.Timeout),
                    Url = api.Uri,
                    WindUnit = api.WindUnit,
                    WindKey = api.WindKey,
                    TemperatureUnit = api.TemperatureUnit,
                    TemperatureKey = api.TemperatureKey,
                });
            }

            return res;
        }

    }

    [ConfigurationCollection(typeof(WebApisInstanceCollection),AddItemName = "api")]
    public class WebApisInstanceCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new WebApiInstanceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            //set to whatever Element Property you want to use for a key
            return ((WebApiInstanceElement)element).Name;
        }
    }
    
    public class WebApiInstanceElement : ConfigurationElement
    {
        //Make sure to set IsKey=true for property exposed as the GetElementKey above
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("uri", IsRequired = true)]
        public string Uri
        {
            get { return (string)base["uri"]; }
            set { base["uri"] = value; }
        }

        [ConfigurationProperty("timeout", IsRequired = false)]
        public int Timeout
        {
            get { return (int)base["timeout"]; }
            set { base["timeout"] = value; }
        }


        [ConfigurationProperty("windUnit", IsRequired = false)]
        public WindUnits WindUnit
        {
            get { return (WindUnits)base["windUnit"]; }
            set { base["windUnit"] = value; }
        }

        [ConfigurationProperty("temperatureUnit", IsRequired = false)]
        public TemperatureUnits TemperatureUnit
        {
            get { return (TemperatureUnits)base["temperatureUnit"]; }
            set { base["temperatureUnit"] = value; }
        }

        [ConfigurationProperty("temperatureKey", IsRequired = true)]
        public string TemperatureKey
        {
            get { return (string)base["temperatureKey"]; }
            set { base["temperatureKey"] = value; }
        }

        [ConfigurationProperty("windKey", IsRequired = true)]
        public string WindKey
        {
            get { return (string)base["windKey"]; }
            set { base["windKey"] = value; }
        }

    }


}