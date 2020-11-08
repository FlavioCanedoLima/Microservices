using System;
using System.Collections.Generic;

namespace Canedo.Identity.Api.Extensions
{
    public static class EnumerableExtension
    {
        public static void InlineForEach<TModel>(this IEnumerable<TModel> collection, Action<TModel> action) where TModel : class
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }
    }
}
