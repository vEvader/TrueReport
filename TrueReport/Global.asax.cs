using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TrueReport.App_Start;

namespace TrueReport
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MapConfig.RegisterMapping();
        }
    }
}
