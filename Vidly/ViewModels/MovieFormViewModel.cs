using System;
using System.Collections.Generic;
using VidlyModels.Models;
using System.Linq;
using System.Web;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Category> CategoryTypes { get; set; }
        public Movie Movie { get; set; }
    }
}