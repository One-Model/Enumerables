using System.Collections.Generic;
using System.Linq;

namespace OneModel.Enumerables
{
    /// <summary>
    /// Extensions to OneModel.Enumerables.VennResult&lt;TLeft, TRight&gt;
    /// </summary>
    public static class VennResultExtensions
    {
        public static IReadOnlyList<T> LeftOrRight<T>(this VennResult<T, T> venn)
            => venn.Left.Concat(venn.Right).ToList();
    }
}
