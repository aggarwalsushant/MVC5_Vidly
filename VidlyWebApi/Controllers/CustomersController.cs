using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using BL = VidlyBL.BusinessLogic;
//using DAL = Vid
using Models = VidlyModels.Models;
using System.Web.Http;

namespace VidlyWebApi.Controllers
{
    //[RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private readonly BL.CustomerBL customerBL = new BL.CustomerBL();

        public CustomersController()
        {

        }
        // GET: api/Customers/1
        [HttpGet]
        //[Route("{id}")]
        public Models.Customer GetCustomer(int id)
        {
            Models.Customer customer = customerBL.GetCustomerDetails(id).FirstOrDefault();
            if (customer==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return customer;
        }

        [HttpGet]
        //[Route("GetVague")]

        public string GetVague() => "Hello Hello!!!";

        [HttpDelete]
        [Route("remove")]
        public void DeleteViaApi(int id)
        {
            customerBL.DeleteCustomer(id);
        }

        // GET: api/Customers
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Customers/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Customers
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Customers/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Customers/5
        //public void Delete(int id)
        //{
        //}
    }
}
