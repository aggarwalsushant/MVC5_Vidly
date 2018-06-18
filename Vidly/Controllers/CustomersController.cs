using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.ViewModels;
using VidlyBL.BusinessLogic;
using VidlyModels.Models;

namespace Vidly.Controllers
{
    [RoutePrefix("Customers")]
    public class CustomersController : Controller
    {
        CustomerBL customerBL = new CustomerBL();
        // GET: Customers
        public ActionResult Index()
        {
            //return View(customerBL.GetCustomerDetails() as IEnumerable<Customer>);
            return View(customerBL.GetAllCustomers() as IEnumerable<Customer>);

        }

        [Route("Details/{id}")]
        public ActionResult GetCustomerDetails(int id)
        {
            IList<Customer> customer = customerBL.GetCustomerDetails(id);
            return View("CustomerDetails", customer.Count>0 ? customer.FirstOrDefault() : null);
        }

        public ActionResult New()
        {
            IList<MembershipType> memTypes = customerBL.GetAllMembershipTypes();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes=memTypes
            };
            return View("CustomerForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            ///adding validation now
            if (!ModelState.IsValid)
            {
                var customerViewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = customerBL.GetAllMembershipTypes()
                };
                return View("CustomerForm", customerViewModel);
            }

            if (customer.Id==0)
                customerBL.SaveCustomer(customer);
            else
                customerBL.UpdateCustomer(customer);

            return RedirectToAction("Index","Customers");
        }

        // Need to figure this out
        //[HttpPut]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(int id)
        {
            Customer customer = customerBL.GetCustomerDetails(id).FirstOrDefault();

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = customerBL.GetAllMembershipTypes()
            };

            return View("CustomerForm",viewModel);
        }
    }
}