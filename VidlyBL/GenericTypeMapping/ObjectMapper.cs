using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;

namespace VidlyBL.GenericTypeMapping
{
    public class Mapper<TSource, TDestination>: IMappingProvider<TSource,TDestination>
    {
        private static readonly Lazy<Mapper<TSource, TDestination>> _instance =
            new Lazy<Mapper<TSource, TDestination>>(() => new Mapper<TSource, TDestination>());

        public static Mapper<TSource, TDestination> Instance => _instance.Value;
        private Mapper() { }

        public TDestination Map(TSource source)
        {
            return source.Adapt<TSource,TDestination>();
        }

        public void MapExisting(TSource source, TDestination destination)
        {
            source.Adapt(destination);
        }
    }

}
