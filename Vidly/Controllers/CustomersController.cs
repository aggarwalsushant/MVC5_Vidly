using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.UtilityData;

namespace Vidly.Controllers
{
    [RoutePrefix("Customers")]
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            return View(DataSimulator.GetAllCustomers());


        }

        [Route("Details/{id}")]
        public ActionResult GetCustomerDetails(int id)
        {
            Customer cust = DataSimulator.GetCustomerData(id);
            return View("CustomerDetails",cust );
        }
    }
}