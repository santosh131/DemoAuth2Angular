using System.Web;
using System.Web.Mvc;

namespace Client_WebApi_Practice_003
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
