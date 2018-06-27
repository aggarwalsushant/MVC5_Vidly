using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VidlyAuth.Controllers
{
    public class HomeController : Controller
    {
        // Without the AllowAnonymous tag, this works as well.
        public ActionResult Index()
        {
            return View();
        }

        // Anybody can access this page.
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // Authorize tag ensures that user is logged in before viewing this
        // else it is shown login page.
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}