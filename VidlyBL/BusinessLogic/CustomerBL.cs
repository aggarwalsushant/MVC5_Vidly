using System.Linq;
using System.Collections.Generic;
using DAL = VidlyDB;
using Models = VidlyModels.Models;
using VidlyBL.GenericTypeMapping;
using VidlyBL.DataAccess;

namespace VidlyBL.BusinessLogic
{
    public class CustomerBL
    {
        DAL.VidlyEntities _dal = VidlyEntitiesSingleton.Instance;
        public IList<Models.Customer> GetCustomerDetails(int id = -1)
        {
            IList<Models.Customer> modelCustomers = new List<Models.Customer>();
            if (id != -1)
            {
                DAL.Customer customer = _dal.Customers.Include("Movies").
                    Where(x => x.CustomerId == id).FirstOrDefault();

                if (customer != null)
                    modelCustomers.Add(CustomerMappingProvider.Instance.Map(customer));
            }
            else
                _dal.Customers.Include("Movies").ToList().ForEach(x => modelCustomers.Add(CustomerMappingProvider.Instance.Map(x)));

            return modelCustomers;
        }
    }
}
