using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidlyBL.GenericTypeMapping
{
    public interface IMappingProvider<TSource, TDestination>
    {
        TDestination Map(TSource input);
    }

}
