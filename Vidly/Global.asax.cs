using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Vidly.AppCode;

namespace Vidly
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //System.Threading.Thread.Sleep(10000);
            // This is custom code for encrypting our connection string 
            // present in the web.config.
            AutoEncryptConnectionStrings.AutoEncrypt();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
