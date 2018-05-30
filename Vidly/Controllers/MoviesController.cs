using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using Vidly.UtilityData;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1"},
                new Customer{ Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel { Movie = movie, Customers = customers };

            return View(viewModel);
            
        }

        public ActionResult Edit(int Id)
        {
            return Content("id=" + Id);
        }

        //movies
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            return View(DataSimulator.GetAllMovies());

        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}