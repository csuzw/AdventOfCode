using System;
using System.Collections.Generic;

namespace AdventOfCode2018
{
    public static class Extensions
    {
        public static IEnumerable<TResult> ZipIfEqual<T, TResult>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, TResult> resultSelector)
        {
            using (var e1 = first.GetEnumerator())
            using (var e2 = second.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext())
                {
                    var v1 = e1.Current;
                    var v2 = e2.Current;
                    if (v1.Equals(v2)) yield return resultSelector(v1, v2);
                }
            }
        }
    }
}
