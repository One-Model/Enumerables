using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace OneModel.Enumerables
{
    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// For convenience. Projects each element of a sequence into a new form,
        /// filtering out any projected object that equals default(TOut).
        /// 
        /// Identical to .Select(t => {...}).Where(t => !t.Equals(default(T)))
        /// </summary>
        public static IEnumerable<TOut> Project<TIn, TOut>(this IEnumerable<TIn> source, Func<TIn, TOut> projection)
        {
            foreach (var item in source)
            {
                var result = projection(item);
                if (!Equals(result, default(TOut)))
                {
                    yield return result;
                }
            }
        }

        /// <summary>
        /// For convenience. Projects each element of a sequence to an IEnumerable&lt;TOut&gt; and
        /// flattens the resulting sequence into one sequence.
        /// 
        /// Identical to .SelectMany(t => {...}).Where(t => !t.Equals(default(T)))
        /// </summary>
        public static IEnumerable<TOut> ProjectMany<TIn, TOut>(this IEnumerable<TIn> source, Func<TIn, IEnumerable<TOut>> projection)
        {
            foreach (var item in source)
            {
                foreach (var item2 in projection(item))
                {
                    if (!Equals(item2, default(TOut)))
                    {
                        yield return item2;
                    }
                }
            }
        }
    }
}
