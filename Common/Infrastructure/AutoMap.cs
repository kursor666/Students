using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Common.Infrastructure
{
    public static class AutoMap<TSource, TDestination> //where TSource : class where TDestination : class
    {
        public static TDestination Map(TSource source, TDestination destination)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (destination == null) throw new ArgumentNullException(nameof(destination));
            Mapper.Initialize(cfg => cfg.CreateMap<TSource, TDestination>());
            Mapper.Map(source, destination);
            return destination;
        }

        public static TDestination Map(TSource source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            Mapper.Initialize(cfg => cfg.CreateMap<TSource, TDestination>());
            TDestination destination = Mapper.Map<TSource, TDestination>(source);
            return destination;
        }
    }
}
