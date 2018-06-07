using System;
using DAL = VidlyDB;
using Models=VidlyModels.Models;

namespace VidlyBL.GenericTypeMapping
{
    public sealed class MovieMappingProvider : IMappingProvider, IConverter<Models.Movie,DAL.Movie>
    {
        #region constructor
        private static readonly Lazy<MovieMappingProvider> _instance =
            new Lazy<MovieMappingProvider>(() => new MovieMappingProvider());

        public static MovieMappingProvider Instance => _instance.Value;
        private MovieMappingProvider() { }
        #endregion

        IConverter<Models.Category, DAL.MovieCategory> movieCategoryMapper = MovieCategoryMappingProvider.Instance;
        IConverter<Models.Customer, DAL.Customer> customerMapper = CustomerMappingProvider.Instance;
        public T Map<T>(IMappable input)
        {
            // TypeB cannot be cast directly from TypeA
            // but it can be cast from object.
            object result = map(input as DAL.Movie);
            return (T)result;
        }

        public DAL.Movie Map(Models.Movie source)
        {
            return new DAL.Movie()
            {
                MovieId = source.Id,
                MovieName = source.Name,
                MovieCategory = movieCategoryMapper.Map(source.CategoryDetails),
                Customer = customerMapper.Map(source.CustomerDetails)
            };
        }

        public Models.Movie Map(DAL.Movie destination)
        {
            return new Models.Movie()
            {
                Id = destination.MovieId,
                Name = destination.MovieName,
                CategoryDetails = movieCategoryMapper.Map(destination.MovieCategory),
                CustomerDetails = customerMapper.Map(destination.Customer)
            };
        }

        private Models.Movie map(DAL.Movie input)
        {
            Models.Movie result = new Models.Movie();
            result.Id = input.MovieId;
            result.Name = input.MovieName;
            result.CategoryDetails = new Models.Category()
            {
                CategoryId = input.CategoryId.HasValue ? input.CategoryId.Value : -1,
                CategoryName =input.MovieCategory.CategoryName
            };
            result.CustomerDetails = new Models.Customer()
            {
                Id = input.CustomerId.HasValue ? input.CategoryId.Value : -1,
                Name = input.Customer.Name,
                Address = input.Customer.Address
            };

            return result;
        }
    }
}
