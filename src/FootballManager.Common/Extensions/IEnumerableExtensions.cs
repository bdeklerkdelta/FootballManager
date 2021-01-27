using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballManager.Common.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool Contains<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Where(predicate).Any();
        }
    }
}
