using VidlyModels.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;

namespace VidlyModels.CustomValidations
{
    internal class Min18YrsIfaMember:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //return base.IsValid(value, validationContext);
            var customer = validationContext.ObjectInstance as Customer;

            if (customer == null)
                return new ValidationResult("Customer details are null");

            if (new int[] { Customer.Unknown, Customer.PayAsYouGo }.Contains(customer.MembershipTypeId))
                return ValidationResult.Success;

            if (customer.BirthDate==null)
                return new ValidationResult("Birth date is required.");

            int age = DateTime.Now.Year - customer.BirthDate.Value.Year;
            return age >= 18 
                ? ValidationResult.Success
                : new ValidationResult("Customer should be of 18 years or older to go for a membership.");
        }
    }
}
