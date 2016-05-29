using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace OneModel.Enumerables
{
    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// Orders and filters a list using a list of IDs.
        /// </summary>
        /// <param name="collection">This list of objects to order</param>
        /// <param name="ids">The ordered list of IDs</param>
        /// <param name="compare">A function that compares an item in the <paramref name="collection" /> with an ID and returns true if a match</param>
        /// <param name="distinct">If true only use a distinct set of IDs. Repeated IDs are ignored</param>
        /// <typeparam name="T">The type of items in the list to be sorted</typeparam>
        /// <typeparam name="TId">The type of the IDs to order and filter with</typeparam>
        /// <returns>A <see cref="IEnumerable{T}" /> collection of the ordered and filtered list</returns>
        /// <remarks>
        /// If an item appears more than once, so will the coresponding item in the returned list.
        /// In this way an item reference can be duplicated.
        /// </remarks>
        public static IEnumerable<T> OrderByIds<T, TId>(
            this IEnumerable<T> collection,
            IEnumerable<TId> ids,
            Func<T, TId, bool> compare,
            bool distinct = false)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (compare == null)
                throw new ArgumentNullException(nameof(compare));

            return (distinct ? ids?.Distinct() : ids)?.Project(id => collection.FirstOrDefault(i => compare(i, id))) ?? new List<T>();
        }

        /// <summary>
        /// Orders and filters a list using a list of IDs.
        /// </summary>
        /// <param name="collection">This list of objects to order</param>
        /// <param name="ids">The ordered list of IDs</param>
        /// <param name="idPredicate">A function that returns the value of an items ID</param>
        /// <param name="distinct">If true only use a distinct set of IDs. Repeated IDs are ignored</param>
        /// <typeparam name="T">The type of items in the list to be sorted</typeparam>
        /// <typeparam name="TId">The type of the IDs to order and filter with</typeparam>
        /// <returns>A <see cref="IEnumerable{T}" /> collection of the ordered and filtered list</returns>
        /// <remarks>
        /// If an item appears more than once, so will the coresponding item in the returned list.
        /// In this way an item reference can be duplicated.
        /// </remarks>
        public static IEnumerable<T> OrderByIds<T, TId>(
            this IEnumerable<T> collection,
            IEnumerable<TId> ids,
            Func<T, TId> idPredicate,
            bool distinct = false)
        {
            if (idPredicate == null)
                throw new ArgumentNullException(nameof(idPredicate));

            return collection.OrderByIds(ids, (t, i) => Equals(idPredicate(t), i), distinct);
        }

        /// <summary>
        /// Orders and filters a list using a list of IDs.
        /// </summary>
        /// <param name="collection">This list of objects to order</param>
        /// <param name="ids">The ordered list of IDs</param>
        /// <param name="idPredicate">A function that returns the value of an items ID</param>
        /// <param name="compare">A function that compares the return value of the <paramref name="idPredicate" /> function and an ID</param>
        /// <param name="distinct">If true only use a distinct set of IDs. Repeated IDs are ignored</param>
        /// <typeparam name="T">The type of items in the list to be sorted</typeparam>
        /// <typeparam name="TId">The type of the IDs to order and filter with</typeparam>
        /// <returns>A <see cref="IEnumerable{T}" /> collection of the ordered and filtered list</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>
        /// If an item appears more than once, so will the coresponding item in the returned list.
        /// In this way an item reference can be duplicated.
        /// </remarks>
        public static IEnumerable<T> OrderByIds<T, TId>(
            this IEnumerable<T> collection,
            IEnumerable<TId> ids,
            Func<T, TId> idPredicate,
            Func<TId, TId, bool> compare,
            bool distinct = false)
        {
            if (idPredicate == null)
                throw new ArgumentNullException(nameof(idPredicate));

            return collection.OrderByIds(ids, (t, i) => compare(idPredicate(t), i), distinct);
        }
    }
}
