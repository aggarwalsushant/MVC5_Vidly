using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace VidlyModels.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Display(Name ="Genre")]
        public string CategoryName { get; set; }
    }
}