using System;
using System.Collections.Generic;
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
        public DateTime ReleaseDate { get; set; }
        public DateTime DateAdded { get; set; }
        public short NumberInStock { get; set; }
    }
}