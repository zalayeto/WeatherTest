using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4ComVacancy.WeatherTest.MicroFramework.Api.Weather
{
    public static class HelperExtensions
    {
        /// <summary>
        /// Conver the temperature value to the specified unit
        /// </summary>
        /// <param name="value"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static float To(this float value, TemperatureUnits unit)
        {
            switch(unit)
            {
                case TemperatureUnits.Celsius:
                    return (float)Math.Round((value - 32) / 1.8, 2);

                case TemperatureUnits.Farenheits:
                    return (float)Math.Round(value * 1.8 + 32, 2);
            }

            throw new Exception("Unit not valid");
        }

        /// <summary>
        /// Conver the wind value to the specified unit
        /// </summary>
        /// <param name="value"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static float To(this float value, WindUnits unit)
        {
            switch(unit)
            {
                case WindUnits.KPHs:
                    return (float)Math.Round(value / 0.62137,2);

                case WindUnits.MPHs:
                    return (float)Math.Round(value * 0.62137, 2);

            }

            throw new Exception("Unit not valid");
        }

    }
}
