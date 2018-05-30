using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //enable attribute routing
            routes.MapMvcAttributeRoutes();

            // One way of creating a custom route. Better way is Attribute routing
            /*
            routes.MapRoute(
                "MoviesByReleaseDate",
                "movies/released/{year}/{month}",
                new { controller = "Movies", action = "ByReleaseDate" },
                new { year=@"\d{4}", month=@"\d{2}"}
                //new { year=@"2015|2016", month=@"\d{2}"} // if you want to fix it to a few values.
            ); // CONSTRAINTS - Regex to enforce a type of input when passing parameter for this route.
            */
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
