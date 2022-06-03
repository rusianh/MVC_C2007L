using System.Web;
using System.Web.Mvc;

namespace Homework_S8_Javascrip_Jquery
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
