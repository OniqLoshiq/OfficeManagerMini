using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;

namespace OMM.Services.AutoMapper
{
    public static class EnumerableMappingExtensions
    {
        public static IEnumerable<TDestination> To<TDestination>(
           this IEnumerable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (var src in source)
            {
                yield return Mapper.Map<TDestination>(src);
            }
        }
    }
}
