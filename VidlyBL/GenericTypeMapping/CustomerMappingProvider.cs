using System;
using DAL = VidlyDB;
using Models=VidlyModels.Models;

namespace VidlyBL.GenericTypeMapping
{
    public sealed class CustomerMappingProvider : IMappingProvider, IConverter<Models.Customer, DAL.Customer>
    {
        #region constructor
        private static readonly Lazy<CustomerMappingProvider> _instance =
            new Lazy<CustomerMappingProvider>(() => new CustomerMappingProvider());

        public static CustomerMappingProvider Instance => _instance.Value;
        private CustomerMappingProvider() {}
        #endregion
        public T Map<T>(IMappable input)
        {
            // TypeB cannot be cast directly from TypeA
            // but it can be cast from object.
            object result = map(input as DAL.Customer);
            return (T)result;
        }

        public DAL.Customer Map(Models.Customer UiCustomerSource)
        {
            return new DAL.Customer()
            {
                CustomerId = UiCustomerSource.Id,
                Name = UiCustomerSource.Name,
                Address = UiCustomerSource.Address
            };
        }

        public Models.Customer Map(DAL.Customer DalCustomerDestination)
        {
            if (DalCustomerDestination != null)
            {
                return new Models.Customer()
                {
                    Id = DalCustomerDestination.CustomerId,
                    Name = DalCustomerDestination.Name,
                    Address = DalCustomerDestination.Address
                };
            }
            return null;
        }

        private Models.Customer map(DAL.Customer input)
        {
            Models.Customer result = new Models.Customer();
            result.Id = input.CustomerId;
            result.Name = input.Name;
            result.Address = input.Address;
            return result;
        }

    }
}
