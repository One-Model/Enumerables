using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace OneModel.Enumerables
{
    public static partial class ListExtensions
    {
        /// <summary>
        /// Appends a single item to the end of the list, 
        /// and returns the same list.
        /// </summary>
        public static TList Append<TList, TItem>(this TList list, TItem item) where TList : IList<TItem>
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            
            list.Add(item);
            return list;
        }

        /// <summary>
        /// Appends a sequence of items to the end of the list,
        /// and returns the same list.
        /// </summary>
        public static TList AppendRange<TList, TItem>(this TList list, IEnumerable<TItem> items) where TList : IList<TItem>
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            list.AddRange(items);
            return list;
        }
    }
}