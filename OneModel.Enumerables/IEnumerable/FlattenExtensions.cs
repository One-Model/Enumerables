using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace OneModel.Enumerables
{
    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// Flattens a tree.
        /// </summary>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> parents, Func<T, IEnumerable<T>> getChildren)
        {
            if (parents == null)
                throw new ArgumentNullException(nameof(parents));
            if (getChildren == null)
                throw new ArgumentNullException(nameof(getChildren));

            var stack = new Stack<T>();
            foreach (var item in parents)
            {
                stack.Push(item);
            }

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                yield return current;

                var children = getChildren(current);
                if (children == null)
                {
                    continue;
                }

                foreach (var child in children)
                {
                    stack.Push(child);
                }
            }
        }
    }
}
