using System;
using DAL = VidlyDB;
using Models=VidlyModels.Models;

namespace VidlyBL.GenericTypeMapping
{
    public sealed class CustomerMappingProvider : IConverter<Models.Customer, DAL.Customer>
    {
        #region constructor
        private static readonly Lazy<CustomerMappingProvider> _instance =
            new Lazy<CustomerMappingProvider>(() => new CustomerMappingProvider());

        public static CustomerMappingProvider Instance => _instance.Value;
        private CustomerMappingProvider() {}
        #endregion
        
        public DAL.Customer Map(Models.Customer UiCustomerSource)
        {
            return new DAL.Customer()
            {
                CustomerId = UiCustomerSource.Id,
                Name = UiCustomerSource.Name,
                Address = UiCustomerSource.Address,
                MembershipType = new DAL.MembershipType()
                {
                    Id = UiCustomerSource.MembershipType.Id,
                    DiscountRate = UiCustomerSource.MembershipType.DiscountRate,
                    DurationInMonths = UiCustomerSource.MembershipType.DurationInMonths,
                    SignUpFee = UiCustomerSource.MembershipType.SignUpFee
                }
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
                    Address = DalCustomerDestination.Address,
                    MembershipType = new Models.MembershipType()
                    {
                        Id = DalCustomerDestination.MembershipType.Id,
                        DiscountRate = DalCustomerDestination.MembershipType.DiscountRate,
                        DurationInMonths = DalCustomerDestination.MembershipType.DurationInMonths,
                        SignUpFee = DalCustomerDestination.MembershipType.SignUpFee
                    }
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
