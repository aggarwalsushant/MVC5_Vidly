using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.ViewModels;
using VidlyBL.BusinessLogic;
using VidlyModels.Models;

namespace Vidly.Controllers
{
    [RoutePrefix("Movies")]
    public class MoviesController : Controller
    {
        MovieBL movieBL = new MovieBL();
        CategoryBL categoryBL = new CategoryBL();
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

        public ActionResult Edit(int id)
        {
            Movie movie = movieBL.GetMovieDetails(id).Single();
            if (movie == null)
                return HttpNotFound();

            MovieFormViewModel movieFormViewModel = new MovieFormViewModel
            {
                Movie = movie,
                CategoryTypes = categoryBL.GetAllCategories()
            };
            return View("MovieForm", movieFormViewModel);
        }

        //[HttpPost]
        public ActionResult SaveMovie(Movie movie)
        {
            if (movie.Id == 0)
            {
                // Fake way of testing whether we are navigating to a new movie page
                // or we are pressing save button on new movie page.
                if (movie.Name == null && movie.ReleaseDate == DateTime.MinValue && movie.CategoryDetails==null)
                {
                    MovieFormViewModel movieFormViewModel = new MovieFormViewModel
                    {
                        CategoryTypes = categoryBL.GetAllCategories()
                    };
                    return View("MovieForm", movieFormViewModel);
                }
                else
                {
                    movieBL.SaveMovie(movie);
                }
            }
            else
                movieBL.UpdateMovie(movie);

            return RedirectToAction("Index", "Movies");
        }


        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}