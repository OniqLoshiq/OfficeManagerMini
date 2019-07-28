using AutoMapper;
using System;

namespace OMM.Services.AutoMapper
{
    public static class ObjectMappingExtensions
    {
        public static T To<T>(this object origin)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }

            return Mapper.Map<T>(origin);
        }
    }
}
