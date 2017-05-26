using System.Web;
using System.Web.Mvc;
using TinyMe.Filters;

namespace TinyMe
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // filters.Add(new HandleErrorAttribute());
            filters.Add(new TinyMeExceptionFilter());
        }
    }
}
