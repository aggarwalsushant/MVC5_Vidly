using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;

namespace VidlyBL.GenericTypeMapping
{
    public class ObjectMapper<TSource, TDestination>: IMappingProvider<TSource,TDestination>
    {
        private static readonly Lazy<ObjectMapper<TSource, TDestination>> _instance =
            new Lazy<ObjectMapper<TSource, TDestination>>(() => new ObjectMapper<TSource, TDestination>());

        public static ObjectMapper<TSource, TDestination> Instance => _instance.Value;
        private ObjectMapper() { }

        public TDestination Map(TSource source)
        {
            return source.Adapt<TSource,TDestination>();
        }
    }

}
