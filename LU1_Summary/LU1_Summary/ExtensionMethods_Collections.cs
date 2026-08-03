using System;
using System.Collections.Generic;
using System.Text;

namespace LU1_Summary
{
    public static class ExtensionMethods_Collections
    {
        //removing null items from an arraylist
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> values) where T : class
        {
            foreach (var item in values)
            {
                if (item != null)
                {
                    yield return item;
                }
            }

        }
    }
}
