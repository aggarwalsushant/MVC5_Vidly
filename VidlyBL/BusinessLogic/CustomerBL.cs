using System.Linq;
using System.Collections.Generic;
using DAL = VidlyDB;
using Models = VidlyModels.Models;
using VidlyBL.GenericTypeMapping;
using VidlyBL.DataAccess;
using Mapster;

namespace VidlyBL.BusinessLogic
{
    public class CustomerBL
    {
        DAL.VidlyEntities _dal = VidlyEntitiesSingleton.Instance;
        static CustomerBL()
        {
            ObjectAdapterConfigurations.RegisterConfigurations();
        }

        public IList<Models.Customer> GetCustomerDetails(int id = -1)
        {
            IList<Models.Customer> modelCustomers = new List<Models.Customer>();
            if (id != -1)
            {
                DAL.Customer customer = _dal.Customers.Include("Movies").Include("MembershipType").
                    Where(x => x.CustomerId == id).FirstOrDefault();

                if (customer != null)
                    modelCustomers.Add(ObjectMapper<DAL.Customer,Models.Customer>.Instance.Map(customer));
            }
            else
                _dal.Customers.Include("Movies").Include("MembershipType")
                    .ToList()
                    .ForEach(x => modelCustomers.Add(ObjectMapper<DAL.Customer, Models.Customer>.Instance.Map(x)));

            return modelCustomers;
        }

        public IList<Models.Customer> GetAllCustomers()
        {
            IList<Models.Customer> modelCustomers = new List<Models.Customer>();
            foreach (var customer in _dal.Customers)
            {
                modelCustomers.Add(ObjectMapper<DAL.Customer,Models.Customer>.Instance.Map(customer));
            }
            return modelCustomers;
        }

        public IList<Models.MembershipType> GetAllMembershipTypes()
        {
            IList<Models.MembershipType> memTypes = new List<Models.MembershipType>();
            foreach (DAL.MembershipType item in _dal.MembershipTypes)
            {
                memTypes.Add(ObjectMapper<DAL.MembershipType, Models.MembershipType>.Instance.Map(item));
            }
            return memTypes;
        }

        public void SaveCustomer(Models.Customer customer)
        {
            try
            {
                customer.Id = _dal.Customers.Max(x => x.CustomerId)+1;
                DAL.Customer dalCust = ObjectMapper<Models.Customer, DAL.Customer>.Instance.Map(customer);
                _dal.Customers.Add(dalCust);
                _dal.SaveChanges();
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}
