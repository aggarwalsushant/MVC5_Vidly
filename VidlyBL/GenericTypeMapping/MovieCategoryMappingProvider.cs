using System;
using DAL = VidlyDB;
using Models = VidlyModels.Models;

namespace VidlyBL.GenericTypeMapping
{
    public sealed class MovieCategoryMappingProvider : IMappingProvider<Models.Category, DAL.MovieCategory>, IConverter<Models.Category, DAL.MovieCategory>
    {
        #region constructor
        private static readonly Lazy<MovieCategoryMappingProvider> _instance =
            new Lazy<MovieCategoryMappingProvider>(() => new MovieCategoryMappingProvider());

        public static MovieCategoryMappingProvider Instance => _instance.Value;
        private MovieCategoryMappingProvider() { }
        #endregion

        public T Map<T>(IMappable input)
        {
            // TypeB cannot be cast directly from TypeA
            // but it can be cast from object.
            object result = map(input as DAL.MovieCategory);
            return (T)result;
        }

        public DAL.MovieCategory Map(Models.Category source)
        {
            return new DAL.MovieCategory()
            {
                CategoryId = source.CategoryId,
                CategoryName = source.CategoryName
            };
        }

        public Models.Category Map(DAL.MovieCategory destination)
        {
            if (destination != null)
            {
                return new Models.Category()
                {
                    CategoryId = destination.CategoryId,
                    CategoryName = destination.CategoryName
                };
            }
            return null;
        }

        private Models.Category map(DAL.MovieCategory input)
        {
            Models.Category result = new Models.Category();
            result.CategoryId = input.CategoryId;
            result.CategoryName = input.CategoryName;
            
            return result;
        }
    }
}
