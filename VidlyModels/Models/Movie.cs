using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VidlyModels.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category CategoryDetails { get; set; }
        public Customer CustomerDetails { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        public DateTime DateAdded { get; set; }

        [Display(Name = "Number in Stock")]
        public short NumberInStock { get; set; }

        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> CustomerId { get; set; }
    }
}