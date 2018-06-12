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
        DAL.VidlyEntities _dal = VidlyEntitiesSingleton.Instance;

        static MovieBL()
        {
            ObjectAdapterConfigurations.RegisterConfigurations();
        }
        public IList<Models.Movie> GetMovieDetails(int id = -1)
        {
            IList<Models.Movie> modelMovies = new List<Models.Movie>();
            if (id != -1)
            {
                DAL.Movie movie = _dal.Movies.Include("Customer").Include("MovieCategory"). // Including all foreign key objects
                    Where(x => x.MovieId == id).FirstOrDefault();

                if (movie != null)
                    modelMovies.Add(ObjectMapper<DAL.Movie,Models.Movie>.Instance.Map(movie));
            }
            else
                _dal.Movies.Include("Customer").Include("MovieCategory").ToList().ForEach(x => modelMovies.Add(ObjectMapper<DAL.Movie, Models.Movie>.Instance.Map(x))); // Including all foreign key objects

            return modelMovies;
        }

        public IList<Models.Movie> GetMovieDetailsBySp(int id=-1)
        {
            IList<Models.Movie> modelMovies = new List<Models.Movie>();

            if (id != -1)
            {
                var movies = _dal.SP_GetMovieById(id);
                if (movies != null && movies.Count() > 0)
                {
                    foreach (DAL.SP_GetMovieById_Result item in movies.ToList())
                    {
                        modelMovies.Add(ObjectMapper<DAL.Movie, Models.Movie>.Instance.Map(
                            ObjectMapper<DAL.SP_GetMovieById_Result, DAL.Movie>.Instance.Map(item)
                            ));
                    }
                }
            }
            else
            {
                var movies = _dal.SP_GetAllMovies();
                if (movies != null)
                {
                    foreach (DAL.SP_GetAllMovies_Result item in movies.ToList())
                    {
                        modelMovies.Add(ObjectMapper<DAL.Movie, Models.Movie>.Instance.Map(
                            ObjectMapper<DAL.SP_GetAllMovies_Result, DAL.Movie>.Instance.Map(item)
                            ));
                    }
                }
            }

            return modelMovies;
        }
    }
}
