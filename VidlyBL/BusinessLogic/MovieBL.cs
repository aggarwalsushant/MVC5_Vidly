using System.Linq;
using System.Collections.Generic;
using DAL = VidlyDB;
using Models = VidlyModels.Models;
using VidlyBL.GenericTypeMapping;
using VidlyBL.DataAccess;
using Mapster;

namespace VidlyBL.BusinessLogic
{
    public class MovieBL
    {
        DAL.VidlyEntities _context = VidlyEntitiesSingleton.Instance;

        static MovieBL()
        {
            ObjectAdapterConfigurations.RegisterConfigurations();
        }
        public IList<Models.Movie> GetMovieDetails(int id = -1)
        {
            IList<Models.Movie> modelMovies = new List<Models.Movie>();
            if (id != -1)
            {
                DAL.Movie movie = _context.Movies.Include("Customer").Include("MovieCategory"). // Including all foreign key objects
                    Where(x => x.MovieId == id).FirstOrDefault();

                if (movie != null)
                    modelMovies.Add(Mapper<DAL.Movie,Models.Movie>.Instance.Map(movie));
            }
            else
                _context.Movies.Include("Customer").Include("MovieCategory").ToList().ForEach(x => modelMovies.Add(Mapper<DAL.Movie, Models.Movie>.Instance.Map(x))); // Including all foreign key objects

            return modelMovies;
        }

        public IList<Models.Movie> GetMovieDetailsBySp(int id=-1)
        {
            IList<Models.Movie> modelMovies = new List<Models.Movie>();

            if (id != -1)
            {
                var movies = _context.SP_GetMovieById(id);
                if (movies != null && movies.Count() > 0)
                {
                    foreach (DAL.SP_GetMovieById_Result item in movies.ToList())
                    {
                        modelMovies.Add(Mapper<DAL.Movie, Models.Movie>.Instance.Map(
                            Mapper<DAL.SP_GetMovieById_Result, DAL.Movie>.Instance.Map(item)
                            ));
                    }
                }
            }
            else
            {
                var movies = _context.SP_GetAllMovies();
                if (movies != null)
                {
                    foreach (DAL.SP_GetAllMovies_Result item in movies.ToList())
                    {
                        modelMovies.Add(Mapper<DAL.Movie, Models.Movie>.Instance.Map(
                            Mapper<DAL.SP_GetAllMovies_Result, DAL.Movie>.Instance.Map(item)
                            ));
                    }
                }
            }

            return modelMovies;
        }

        public void SaveMovie(Models.Movie movie)
        {
            try
            {
                movie.Id = _context.Movies.Max(x => x.MovieId) + 1;
                movie.DateAdded = System.DateTime.Now;
                DAL.Movie dalMovie = Mapper<Models.Movie, DAL.Movie>.Instance.Map(movie);
                _context.Movies.Add(dalMovie);
                _context.SaveChanges();
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public void UpdateMovie(Models.Movie movie)
        {
            try
            {
                DAL.Movie dalMovie = _context.Movies.Where(x => x.MovieId == movie.Id).Single();
                
                dalMovie.CategoryId = movie.CategoryId;
                dalMovie.MovieName = movie.Name;
                dalMovie.ReleaseDate = movie.ReleaseDate;
                dalMovie.NumberInStock = movie.NumberInStock;
                _context.SaveChanges();
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}
