﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidlyBL.GenericTypeMapping
{
    public interface IMappable
    {

    }
    public interface IMappingProvider<TSource, TDestination>
    {
        TDestination Map(TSource input);
    }

    public interface IConverter<TSource,TDestination>
    {
        TDestination Map(TSource source);

        TSource Map(TDestination destination);
    }
    
}
