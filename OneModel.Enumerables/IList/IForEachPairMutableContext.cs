namespace OneModel.Enumerables
{
    public interface IForeachPairMutableContext<T>
    {
        int LhsIndex { get; }

        int RhsIndex { get; }

        T Lhs { get; }

        T Rhs { get; }

        /// <summary>
        /// Replace the left hand side of the pair with a new value.
        /// </summary>
        void ReplaceLhs(T newLhs);

        /// <summary>
        /// Replaces the right hand side of the pair with a new value.
        /// </summary>
        /// <param name="newRhs"></param>
        void ReplaceRhs(T newRhs);

        /// <summary>
        /// Remove the left hand side of the pair.
        /// </summary>
        void RemoveLhs();

        /// <summary>
        /// Remove the right hand side of the pair.
        /// </summary>
        void RemoveRhs();

        /// <summary>
        /// Remove both the left hand side and right hand side of the pair.
        /// </summary>
        void RemoveBoth();
    }
}