using System.Web;
using System.Web.Mvc;

namespace _4ComVacancy.WeatherTest.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
