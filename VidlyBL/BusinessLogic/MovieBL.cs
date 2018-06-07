using System.Linq;
using System.Collections.Generic;
using DAL = VidlyDB;
using Models = VidlyModels.Models;
using VidlyBL.GenericTypeMapping;
using VidlyBL.DataAccess;

namespace VidlyBL.BusinessLogic
{
    public class MovieBL
    {
        DAL.VidlyEntities _dal = VidlyEntitiesSingleton.Instance;
        public IList<Models.Movie> GetMovieDetails(int id = -1)
        {
            IList<Models.Movie> modelMovies = new List<Models.Movie>();
            if (id != -1)
            {
                DAL.Movie movie = _dal.Movies.Include("Customer").Include("MovieCategory"). // Including all foreign key objects
                    Where(x => x.MovieId == id).FirstOrDefault();

                if (movie != null)
                    modelMovies.Add(MovieMappingProvider.Instance.Map(movie));
            }
            else
                _dal.Movies.Include("Customer").Include("MovieCategory").ToList().ForEach(x => modelMovies.Add(MovieMappingProvider.Instance.Map(x))); // Including all foreign key objects

            return modelMovies;
        }
    }
}
