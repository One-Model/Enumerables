using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace OneModel.Enumerables
{
    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// Similar to IEnumerable.Min(), but instead of returning the value of the projection, returns the 
        /// source item. e.g. if you have a type like this:
        /// 
        ///   class MyType { int Order; }
        /// 
        /// and an enumeration of them like this:
        /// 
        ///   IEnumerable&lt;MyType&gt; myList;
        /// 
        /// This call would return an integer:
        /// 
        ///   myList.Min(x => x.Order)
        /// 
        /// Whereas this call would return an isntance of MyType
        /// 
        ///   myList.Min2(x => x.Order)
        /// 
        /// </summary>
        public static TIn Min2<TIn, TCompare>(
            this IEnumerable<TIn> source,
            Func<TIn, TCompare> projectFn,
            MinMaxEmptyBehavior behavior = MinMaxEmptyBehavior.ReturnDefault) where TCompare : IComparable
        {
            var first = true;
            var minItem = default(TIn);
            var minValue = default(TCompare);

            foreach (var item in source)
            {
                var value = projectFn(item);
                var compare = value.CompareTo(minValue);
                if (first || compare == -1)
                {
                    minItem = item;
                    minValue = value;
                }
                first = false;
            }

            if (first && behavior == MinMaxEmptyBehavior.ThrowException)
            {
                throw new ArgumentException($"source enumerable was empty", nameof(source));
            }

            return minItem;
        }
    }
}
