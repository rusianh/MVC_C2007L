using System.Web;
using System.Web.Mvc;

namespace EAP_C2007L_NguyenDucHuy
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
