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
        DAL.VidlyEntities _context = VidlyEntitiesSingleton.Instance;

        static CustomerBL()
        {
            ObjectAdapterConfigurations.RegisterConfigurations();
        }

        public IList<Models.Customer> GetCustomerDetails(int id = -1)
        {
            IList<Models.Customer> modelCustomers = new List<Models.Customer>();
            if (id != -1)
            {
                DAL.Customer customer = _context.Customers.Include("Movies").Include("MembershipType").
                    Where(x => x.CustomerId == id).FirstOrDefault();

                if (customer != null)
                    modelCustomers.Add(Mapper<DAL.Customer,Models.Customer>.Instance.Map(customer));
            }
            else
                _context.Customers.Include("Movies").Include("MembershipType")
                    .ToList()
                    .ForEach(x => modelCustomers.Add(Mapper<DAL.Customer, Models.Customer>.Instance.Map(x)));

            return modelCustomers;
        }

        public IList<Models.Customer> GetAllCustomers()
        {
            IList<Models.Customer> modelCustomers = new List<Models.Customer>();
            foreach (var customer in _context.Customers)
            {
                modelCustomers.Add(Mapper<DAL.Customer,Models.Customer>.Instance.Map(customer));
            }
            return modelCustomers;
        }

        public IList<Models.MembershipType> GetAllMembershipTypes()
        {
            IList<Models.MembershipType> memTypes = new List<Models.MembershipType>();
            foreach (DAL.MembershipType item in _context.MembershipTypes)
            {
                memTypes.Add(Mapper<DAL.MembershipType, Models.MembershipType>.Instance.Map(item));
            }
            return memTypes;
        }

        public void SaveCustomer(Models.Customer customer)
        {
            try
            {
                customer.Id = _context.Customers.Max(x => x.CustomerId)+1;
                DAL.Customer dalCust = Mapper<Models.Customer, DAL.Customer>.Instance.Map(customer);
                _context.Customers.Add(dalCust);
                _context.SaveChanges();
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public void UpdateCustomer(Models.Customer customer)
        {
            try
            {
                DAL.Customer cust = _context.Customers.Where(x => x.CustomerId == customer.Id).Single();
                Mapper<Models.Customer, DAL.Customer>.Instance.MapExisting(customer, cust);
                _context.SaveChanges();
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}
