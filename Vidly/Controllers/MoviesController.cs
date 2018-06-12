using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VidlyModels.Models;
using Vidly.ViewModels;
using Vidly.UtilityData;
using VidlyModels.Models;
using VidlyBL.BusinessLogic;

namespace Vidly.Controllers
{
    [RoutePrefix("Movies")]
    public class MoviesController : Controller
    {
        MovieBL movieBL = new MovieBL();
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

        //movies
        public ActionResult Index()
        {
            return View(movieBL.GetMovieDetails() as IEnumerable<Movie>);
        }

        [Route("Details/{id}")]
        public ActionResult Details(int id)
        {
            IList<Movie> list = movieBL.GetMovieDetails(id);
            return View("MovieDetails",list.Count>0 ? list.FirstOrDefault():null);
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}