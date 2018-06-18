using System;
using System.ComponentModel.DataAnnotations;
using VidlyModels.CustomValidations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VidlyModels.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Address { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipType MembershipType { get; set; }

        [Display(Name="Membership Type")]
        public byte MembershipTypeId { get; set; }

        [Display(Name="Date of Birth")]
        [Min18YrsIfaMember]
        public DateTime? BirthDate { get; set; }

        // Avoiding magic numbers with static fields or could be used with enum
        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;

    }
}